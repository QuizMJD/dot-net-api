// Controller: viết trong TonKhoController.cs
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/tonkho")]
public class TonKhoController : ControllerBase
{
    [HttpGet]
    public IActionResult GetAll() => Ok("Danh sách tồn kho");
}