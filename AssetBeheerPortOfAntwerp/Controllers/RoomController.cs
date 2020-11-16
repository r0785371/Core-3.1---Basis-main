using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DAL;
using Models;
using BLL.interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PortOfAntwerpAppAssets.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService service;

        public RoomController(IRoomService _service)
        {
            service = _service;
        }

        // GET: Room
        public IActionResult Index()
        {
            List<Room> rooms = service.GetAllRooms();
            return View(rooms);
        }

        // GET: Room/Details/
        
        public IActionResult Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Room room = service.FindById(id.Value);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Room/Create
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Room/Create       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Create([Bind("RoomID,Ref,Name")] Room room)
        {
            if (ModelState.IsValid)
            {
                service.Add(room);
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Room/Edit/
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Room room = service.FindById(id.Value);
            Tuple<long, Room, List<Location>> room = service.GetAllRoomsWithLocations(id.Value);

            if (room == null)
            {
                return NotFound();
            }

            int qtyLocation = room.Item3.Count();
            ViewData["Qty"] = qtyLocation != 0 ? qtyLocation.ToString() : "0";
            ViewData["ListLocations"] = new List<Location>(room.Item3);

            return View(room.Item2);
        }

        // POST: Room/Edit/        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD,UserCRU")]
        public IActionResult Edit(long id, [Bind("RoomID,Ref,Name")] Room room)
        {
            if (id != room.RoomID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    service.Update(room);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.RoomID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        // GET: Room/Delete/
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Room room = service.FindById(id.Value);

            Tuple<long, Room, List<Location>> room = service.GetAllRoomsWithLocations(id.Value);

            if (room == null)
            {
                return NotFound();
            }
            int qtyLocation = room.Item3.Count();

            ViewData["Qty"] = qtyLocation != 0 ? qtyLocation.ToString() : "0";
            ViewData["ListLocations"] = new List<Location>(room.Item3);

            return View(room.Item2);
        }

        // POST: Room/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator,UserCRUD")]
        public IActionResult DeleteConfirmed(long id)
        {
            service.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(long id)
        {
            return service.RoomExists(id);
        }
    }
}
