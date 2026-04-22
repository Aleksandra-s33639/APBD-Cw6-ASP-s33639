using Microsoft.AspNetCore.Mvc;
using APBD_Cw6_ASP_s33639.Data;
using APBD_Cw6_ASP_s33639.Models;

namespace APBD_Cw6_ASP_s33639.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAll(
        [FromQuery] DateOnly? date,
        [FromQuery] string? status,
        [FromQuery] int? roomId)
    {
        IEnumerable<Reservation> reservations = InMemoryDataStore.Reservations;

        if (date.HasValue)
        {
            reservations = reservations.Where(r => r.Date == date.Value);
        }

        if (!string.IsNullOrWhiteSpace(status))
        {
            reservations = reservations.Where(r =>
                r.Status.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        if (roomId.HasValue)
        {
            reservations = reservations.Where(r => r.RoomId == roomId.Value);
        }

        return Ok(reservations);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetById(int id)
    {
        var reservation = InMemoryDataStore.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult<Reservation> Create([FromBody] Reservation reservation)
    {
        var room = InMemoryDataStore.Rooms.FirstOrDefault(r => r.Id == reservation.RoomId);

        if (room is null)
        {
            return BadRequest(new
            {
                message = "Cannot create reservation for a room that does not exist."
            });
        }

        if (!room.IsActive)
        {
            return BadRequest(new
            {
                message = "Cannot create reservation for an inactive room."
            });
        }

        var hasConflict = InMemoryDataStore.Reservations.Any(r =>
            r.RoomId == reservation.RoomId &&
            r.Date == reservation.Date &&
            r.Status.ToLower() != "cancelled" &&
            reservation.Status.ToLower() != "cancelled" &&
            reservation.StartTime < r.EndTime &&
            reservation.EndTime > r.StartTime);

        if (hasConflict)
        {
            return Conflict(new
            {
                message = "Reservation time conflicts with an existing reservation."
            });
        }

        var newId = InMemoryDataStore.Reservations.Count == 0
            ? 1
            : InMemoryDataStore.Reservations.Max(r => r.Id) + 1;

        reservation.Id = newId;
        InMemoryDataStore.Reservations.Add(reservation);

        return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
    }

    [HttpPut("{id:int}")]
    public ActionResult<Reservation> Update(int id, [FromBody] Reservation updatedReservation)
    {
        var existingReservation = InMemoryDataStore.Reservations.FirstOrDefault(r => r.Id == id);

        if (existingReservation is null)
        {
            return NotFound();
        }

        var room = InMemoryDataStore.Rooms.FirstOrDefault(r => r.Id == updatedReservation.RoomId);

        if (room is null)
        {
            return BadRequest(new
            {
                message = "Cannot assign reservation to a room that does not exist."
            });
        }

        if (!room.IsActive)
        {
            return BadRequest(new
            {
                message = "Cannot assign reservation to an inactive room."
            });
        }

        var hasConflict = InMemoryDataStore.Reservations.Any(r =>
            r.Id != id &&
            r.RoomId == updatedReservation.RoomId &&
            r.Date == updatedReservation.Date &&
            r.Status.ToLower() != "cancelled" &&
            updatedReservation.Status.ToLower() != "cancelled" &&
            updatedReservation.StartTime < r.EndTime &&
            updatedReservation.EndTime > r.StartTime);

        if (hasConflict)
        {
            return Conflict(new
            {
                message = "Updated reservation conflicts with an existing reservation."
            });
        }

        existingReservation.RoomId = updatedReservation.RoomId;
        existingReservation.OrganizerName = updatedReservation.OrganizerName;
        existingReservation.Topic = updatedReservation.Topic;
        existingReservation.Date = updatedReservation.Date;
        existingReservation.StartTime = updatedReservation.StartTime;
        existingReservation.EndTime = updatedReservation.EndTime;
        existingReservation.Status = updatedReservation.Status;

        return Ok(existingReservation);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
        var reservation = InMemoryDataStore.Reservations.FirstOrDefault(r => r.Id == id);

        if (reservation is null)
        {
            return NotFound();
        }

        InMemoryDataStore.Reservations.Remove(reservation);
        return NoContent();
    }
}