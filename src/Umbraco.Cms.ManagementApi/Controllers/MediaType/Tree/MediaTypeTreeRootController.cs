﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.ManagementApi.ViewModels.Tree;

namespace Umbraco.Cms.ManagementApi.Controllers.MediaType.Tree;

public class MediaTypeTreeRootController : MediaTypeTreeControllerBase
{
    public MediaTypeTreeRootController(IEntityService entityService, IMediaTypeService mediaTypeService)
        : base(entityService, mediaTypeService)
    {
    }

    [HttpGet("root")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PagedResult<FolderTreeItemViewModel>), StatusCodes.Status200OK)]
    public async Task<ActionResult<PagedResult<FolderTreeItemViewModel>>> Root(long pageNumber = 0, int pageSize = 100, bool foldersOnly = false)
        => await GetRoot(pageNumber, pageSize, foldersOnly);
}
