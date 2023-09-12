using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Http.Headers;
using ClosedXML;
using DocumentFormat.OpenXml.Math;
using GoodlistInsert.Interfaces;
using GoodlistInsert.Models;
using GoodlistInsert.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GoodlistInsert.Controllers;

[ApiController]
public class ListController : Controller
{

    private readonly IListService _listService;


    public ListController(IListService listService)
    {
        _listService = listService;
    }

    [HttpPost("Inserir")]
    public IActionResult InserirWhitelist (IFormFile file, [FromForm] vwObjectQuery obj)
    {
             _listService.CreateListScript(obj, file);

            return Ok();
    }


    [HttpPost("Alterar")]
    public IActionResult AlterarWhitelist (IFormFile file, [FromForm] vwObjectQuery obj)
    {

        _listService.CreateUpdateListScript(obj, file);

        return Ok();
    }
}

