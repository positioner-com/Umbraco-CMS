// Copyright (c) Umbraco.
// See LICENSE for more details.

using Umbraco.Cms.Core.IO;

namespace Umbraco.Cms.Core.PropertyEditors;

/// <summary>
/// Represents a block list property editor.
/// </summary>
[DataEditor(
    Constants.PropertyEditors.Aliases.BlockGrid,
    "Block Grid",
    "blockgrid",
    ValueType = ValueTypes.Json,
    Group = Constants.PropertyEditors.Groups.Lists,
    Icon = "icon-thumbnail-list")]
public class BlockGridPropertyEditorPropertyEditor : BlockGridPropertyEditorBase
{
    private readonly IIOHelper _ioHelper;

    public BlockGridPropertyEditorPropertyEditor(
        IDataValueEditorFactory dataValueEditorFactory,
        IIOHelper ioHelper)
        : base(dataValueEditorFactory) =>
        _ioHelper = ioHelper;

    #region Pre Value Editor

    protected override IConfigurationEditor CreateConfigurationEditor() => new BlockGridConfigurationEditor(_ioHelper);

    #endregion
}
