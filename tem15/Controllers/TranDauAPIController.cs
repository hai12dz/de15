using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tem15.Models;
using tem15.Models.TranDauModels;

namespace tem15.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranDauAPIController : ControllerBase
    {


        QlgiaiBongDaContext db = new QlgiaiBongDaContext();

        [HttpGet("{tencauthu}")]
        public IEnumerable<TranDauTheoTenCauThu> GetCauThuTheoMa(string tencauthu)
        {
            var lstTranDauTheoTenCauThu = db.Cauthus.AsNoTracking()
                .Where(x => x.HoVaTen == tencauthu)
                .Select(x => x.CauThuId)
                .ToList();

            var lstIdTranDauTheoIdCauThu = db.TrandauCauthus
                .Where(p => lstTranDauTheoTenCauThu.Contains(p.CauThuId))
                .Select(p => p.TranDauId)  // Giả sử TranDauId là thuộc tính của TrandauCauthus
                .Distinct()
                .ToList();


            var tranDaus = db.Trandaus.AsNoTracking()
       .Where(x => lstIdTranDauTheoIdCauThu.Contains(x.TranDauId))
       .Select(x => new TranDauTheoTenCauThu
       {
           Anh = x.Anh, // Giả sử Anh là một thuộc tính của Trandaus
           TranDauId = x.TranDauId
       })
       .ToList();





            return tranDaus;
        }
    }









}

