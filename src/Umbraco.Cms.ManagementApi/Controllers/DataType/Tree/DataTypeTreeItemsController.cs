﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.ManagementApi.ViewModels.Tree;

namespace Umbraco.Cms.ManagementApi.Controllers.DataType.Tree;

public class DataTypeTreeItemsController : DataTypeTreeControllerBase
{
    public DataTypeTreeItemsController(IEntityService entityService, IDataTypeService dataTypeService)
        : base(entityService, dataTypeService)
    {
    }

    [HttpGet("items")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PagedResult<FolderTreeItemViewModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<FolderTreeItemViewModel>>> Items([FromQuery(Name = "key")] Guid[] keys)
        => await GetItems(keys);
}
