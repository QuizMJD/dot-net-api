// Controller: viết trong TonKhoController.cs
using System.Data;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using Dapper;
using Dapper.Oracle;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

[ApiController]
[Route("api/tonkho")]
public class TonKhoController : ControllerBase
{

    private readonly IConfiguration _config;
    public TonKhoController(IConfiguration config)
    {
        _config = config;
    }
    [HttpGet]
    // public IActionResult GetAll() => Ok("Danh sách tồn kho");
    // public ActionResult<List<UserDTO >>GetAll()
    // {

    //     // UserDTO dsa=new UserDTO();
    //     // dsa.age="23";
    //     // dsa.name="qui";
    //     var dsa=new List<UserDTO>
    //     {
    //     new UserDTO { Name = "qui", Age = "25" },
    //     new UserDTO { Name = "kk", Age = "25" }
            
    //     };
    //     return Ok(dsa);
    // }
    public async Task<IActionResult> GetAll()
    {
        var conn =_config.GetConnectionString("Oracle");
        using var db = new OracleConnection(conn);
        try
        {
        //     var ds = await db.QueryAsync<TonKhoDto>(
        //     """
        //     SELECT MA_SP AS "MaSp",
        //            TEN_SP AS "TenSp",
        //            SOLUONG_TON AS "SoLuongTon"
        //     FROM PDBADMIN.TON_KHO
        //     """
        // );
                var thamSo = new OracleDynamicParameters
        {
            BindByName = true
        };

        thamSo.Add(
            "p_du_lieu",
            dbType: OracleMappingType.RefCursor,
            direction: ParameterDirection.Output
        );

        var ds = await db.QueryAsync<TonKhoDto>(
        "PDBADMIN.PKG_TON_KHO_DEMO.P_DANH_SACH",
        thamSo,
        commandType: CommandType.StoredProcedure
);
         return Ok(ds);
        }
        catch (OracleException ex)
{
    Console.WriteLine($"ORA-{ex.Number}: {ex.Message}");
    throw;
}
        
    
    
    
}
public class UserDTO
{
    public string? Name{ get; set; }
    public string? Age{ get; set; }

}
public class TonKhoDto
{
    public string MaSp { get; set; } = "";
    public string TenSp { get; set; } = "";
    public decimal SoLuongTon { get; set; }
}
}
