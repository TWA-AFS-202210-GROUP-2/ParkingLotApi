namespace ParkingLotApi.Controllers;

using Microsoft.AspNetCore.Mvc;
using ParkingLotApi.Dtos;
using ParkingLotApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("parkingLots")]
public class ParkingLotController : ControllerBase
{
    private ParkingLotService parkingLotService;

    public ParkingLotController(ParkingLotService parkingLotService)
    {
        this.parkingLotService = parkingLotService;
    }

    [HttpPost]
    public async Task<ActionResult<ParkingLotDto>> AddParkingLot(ParkingLotDto parkingLotDto)
    {
        if (parkingLotDto.Capcity < 0)
        {
            return BadRequest(new
            {
                message = $"Should input positive capacity."
            });
        }
        var id = await parkingLotService.AddParkingLot(parkingLotDto);
        if(id == -1)
        {
            return BadRequest(new
            {
                message = $"The name of ${parkingLotDto.Name} already exists."
            });
        }
        return CreatedAtAction(nameof(GetParkingLotById),new {id =id}, parkingLotDto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParkingLotDto>>> GetAllParkingLots([FromQuery]int? startPage)
    {
        if(startPage != null)
        {
            return Ok(await parkingLotService.GetByPage((int)startPage));
        }
        var parkingLotDtos = await parkingLotService.GetAllParkingLots();
        return Ok(parkingLotDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ParkingLotDto>> GetParkingLotById(int id)
    {
        var parkingLotDto = await parkingLotService.GetParkingLotById(id);
        return Ok(parkingLotDto);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteParkingLotById(int id)
    {
        await parkingLotService.DeleteParkingLotById(id);

        return this.NoContent();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ParkingLotDto>> UpdateParkingLotById(ParkingLotDto parkingLotDto,[FromRoute]int id)
    {
        var result = await parkingLotService.UpdateParkingLotById(parkingLotDto,id);
        return Ok(result);
    }

    [HttpPost("{id}/orders")]
    public async Task<ActionResult<OrderDto>> AddOrderToParkingLot(OrderDto orderDto, [FromRoute] int id)
    {
        var orderId = await parkingLotService.AddOrder(orderDto, id);
        return Created("{id}/orders",orderDto);
        /*return CreatedAtAction(nameof(GetOrderById), new { orderId = orderId }, orderDto);*/
    }

    [HttpGet("{id}/orders")]
    public async Task<ActionResult<List<OrderDto>>> GetOrderById([FromRoute] int id)
    {
        var getOrderDtos = await parkingLotService.GetOrder(id);
        return Ok(getOrderDtos);
    }
}