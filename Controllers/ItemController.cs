using System.Collections.Generic;
using System.Linq;
//inventory is from databasecontext namespace
using inventory;
using InventoryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ItemController : ControllerBase
  {
    private DatabaseContext context;

    public ItemController(DatabaseContext _context)
    {
      this.context = _context;
    }

    //post request: add item
    [HttpPost("PostItem")]
    public ActionResult<Item> CreateEntry([FromBody]Item entry)
    {
      //added
      context.Items.Add(entry);
      // saved
      context.SaveChanges();
      return entry;
    }

    //get request: return stuff
    //IEnumerable is a list of lists, it is not an actual list
    [HttpGet("GetAllItems")]
    public ActionResult<IEnumerable<Item>> GetEverything()
    {
      // find 
      var items = context.Items.OrderByDescending(item => item.Id);
      // 3. return list
      return items.ToList();
    }

    //get one item and display error message if not found
    [HttpGet("OneItem")]
    public ActionResult GetOneItem(int id)
    {
      //Do something
      var item = context.Items.FirstOrDefault(i => i.Id == id);
      //check return
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        return Ok(item);
      }
    }

    //put request: let client update everything about item
    [HttpPut("UpdateItem")]
    public ActionResult<Item> UpdateItem([FromBody]Item entry, int id)
    {

      {
        //make sure ids match; entry and id
        //otherwise you are not updating you are changing ids
        if (id != entry.Id)
        {
          return BadRequest();
        }
      }
      //modify
      context.Entry(entry).State = EntityState.Modified;
      //save update
      context.SaveChanges();
      //return update
      return entry;
    }

    //delete request: allow client to delete an item
    [HttpDelete("{id}")]
    public ActionResult DeleteItem(int id)
    {
      //find item to delete
      var item = context.Items.FirstOrDefault(i => i.Id == id);
      //return something if it's not there
      if (item == null)
      {
        return NotFound();
      }
      else
      {
        //otherwise delete item
        context.Items.Remove(item);
        //save item
        context.SaveChanges();
        //return a message, cannot return something that was deleted
        return Ok(new { Message = "Item was deleted", item = item });
      }
    }
  }
}