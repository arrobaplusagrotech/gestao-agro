using AgroTech.BusinessLogicLayer.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AgroTech.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ControlPanelController : ControllerBase
    {
        private readonly IControlPanelBLL _controlPanelBLL;

        public ControlPanelController(IControlPanelBLL controlPanelBLL)
        {
            _controlPanelBLL = controlPanelBLL;
        }

        [Route("v1/global-farms"), HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var globalFarmDataReporter = await _controlPanelBLL.Get();
                return Ok(globalFarmDataReporter);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        //[Route("v1/farms/{database}/information-animals"), HttpGet]
        //public async Task<IActionResult> GetInformationAnimals(string dataBase)
        //{
        //    try
        //    {
        //        var farmAnimalsInformations = await _farmInformationBLL.GetInfoAnimals(dataBase);
        //        return Ok(farmAnimalsInformations);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[Route("v1/farms/information-animals"), HttpGet]
        //public async Task<IActionResult> GetAllInformationAnimals()
        //{
        //    try
        //    {
        //        var farmAnimalsInformations = await _farmInformationDefaultBLL.GetInfoAllDataBase();
        //        return Ok(farmAnimalsInformations);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        //    }
        //}

        //[Route("v1/farms/information-animals/charts"), HttpGet]
        //public async Task<IActionResult> GetInfoAnimalsChart()
        //{
        //    try
        //    {
        //        var farmAnimalsInformationsChart = await _farmInformationDefaultBLL.GetInfoAnimalsChart();
        //        return Ok(farmAnimalsInformationsChart);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message));
        //    }
        //}

        //[Route("v1/farms/{farm}/information-animals/charts"), HttpGet]
        //public async Task<IActionResult> GetInformationAboutFarmAnimalsForChart(string farm)
        //{
        //    try
        //    {
        //        var farmAnimalsInformationsChart = await _farmInformationBLL.GetInfoAboutFarmAnimalsChart(farm);
        //        return Ok(farmAnimalsInformationsChart);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse(StatusCodes.Status500InternalServerError, ex.Message));
        //    }
        //}
    }
}
