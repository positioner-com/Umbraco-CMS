﻿using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.ManagementApi.ViewModels.Languages;
using Umbraco.New.Cms.Core.Services.Installer;

namespace Umbraco.Cms.ManagementApi.Controllers.Language;

[ApiVersion("1.0")]
public class UpdateLanguageController : LanguageControllerBase
{
    private readonly ILanguageService _languageService;
    private readonly IUmbracoMapper _umbracoMapper;
    private readonly ILocalizationService _localizationService;

    public UpdateLanguageController(ILanguageService languageService, IUmbracoMapper umbracoMapper, ILocalizationService localizationService)
    {
        _languageService = languageService;
        _umbracoMapper = umbracoMapper;
        _localizationService = localizationService;
    }

    /// <summary>
    ///     Creates or saves a language
    /// </summary>
    [HttpPut("update")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    // TODO: This needs to be an authorized endpoint.
    public async Task<ActionResult<LanguageViewModel?>> Update(LanguageViewModel language)
    {
        ILanguage? existingById = language.Id != default ? _localizationService.GetLanguageById(language.Id) : null;
        if (existingById is null)
        {
            // Someone is trying to update a language that doesn't exist
            ModelState.AddModelError("IsoCode", "The language " + language.IsoCode + " does not exist");
            return ValidationProblem(ModelState);
        }

        // note that the service will prevent the default language from being "un-defaulted"
        // but does not hurt to test here - though the UI should prevent it too
        if (existingById.IsDefault && !language.IsDefault)
        {
            ModelState.AddModelError("IsDefault", "Cannot un-default the default language.");
            return ValidationProblem(ModelState);
        }

        existingById = _umbracoMapper.Map(language, existingById);

        if (!_languageService.CanUseLanguagesFallbackLanguage(existingById))
        {
            ModelState.AddModelError("FallbackLanguage", "The selected fall back language does not exist.");
            return ValidationProblem(ModelState);
        }

        if (!_languageService.CanGetProperFallbackLanguage(existingById))
        {
            ModelState.AddModelError("FallbackLanguage", $"The selected fall back language {_localizationService.GetLanguageById(existingById.FallbackLanguageId!.Value)} would create a circular path.");
            return ValidationProblem(ModelState);
        }

        _localizationService.Save(existingById);
        return _umbracoMapper.Map<LanguageViewModel>(existingById);
    }
}
