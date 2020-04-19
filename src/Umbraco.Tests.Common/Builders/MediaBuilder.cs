using System;
using Umbraco.Core.Models;
using Umbraco.Tests.Common.Builders.Interfaces;

namespace Umbraco.Tests.Common.Builders
{
    public class MediaBuilder
        : BuilderBase<Media>,
            IBuildContentTypes,
            IWithIdBuilder,
            IWithKeyBuilder,
            IWithParentIdBuilder,
            IWithCreatorIdBuilder,
            IWithCreateDateBuilder,
            IWithUpdateDateBuilder,
            IWithNameBuilder,
            IWithTrashedBuilder,
            IWithLevelBuilder,
            IWithPathBuilder,
            IWithSortOrderBuilder
    {
        private MediaTypeBuilder _mediaTypeBuilder;
        private GenericDictionaryBuilder<MediaBuilder, string, object> _propertyDataBuilder;

        private int? _id;
        private Guid? _key;
        private DateTime? _createDate;
        private DateTime? _updateDate;
        private int? _parentId;
        private string _name;
        private int? _creatorId;
        private int? _level;
        private string _path;
        private int? _sortOrder;
        private bool? _trashed;

        public MediaTypeBuilder AddMediaType()
        {
            var builder = new MediaTypeBuilder(this);
            _mediaTypeBuilder = builder;
            return builder;
        }

        public override Media Build()
        {
            var id = _id ?? 1;
            var key = _key ?? Guid.NewGuid();
            var parentId = _parentId ?? -1;
            var createDate = _createDate ?? DateTime.Now;
            var updateDate = _updateDate ?? DateTime.Now;
            var name = _name ?? Guid.NewGuid().ToString();
            var creatorId = _creatorId ?? 1;
            var level = _level ?? 1;
            var path = _path ?? "-1";
            var sortOrder = _sortOrder ?? 0;
            var trashed = _trashed ?? false;

            if (_mediaTypeBuilder == null)
            {
                throw new InvalidOperationException("A member cannot be constructed without providing a member type. Use AddMediaType().");
            }

            var memberType = _mediaTypeBuilder.Build();

            var member = new Media(name, parentId, memberType)
            {
                Id = id,
                Key = key,
                CreateDate = createDate,
                UpdateDate = updateDate,
                CreatorId = creatorId,
                Level = level,
                Path = path,
                SortOrder = sortOrder,
                Trashed = trashed,
            };

            if (_propertyDataBuilder != null)
            {
                var propertyData = _propertyDataBuilder.Build();
                foreach (var kvp in propertyData)
                {
                    member.SetValue(kvp.Key, kvp.Value);
                }

                member.ResetDirtyProperties(false);
            }

            return member;
        }

        int? IWithIdBuilder.Id
        {
            get => _id;
            set => _id = value;
        }

        Guid? IWithKeyBuilder.Key
        {
            get => _key;
            set => _key = value;
        }

        int? IWithCreatorIdBuilder.CreatorId
        {
            get => _creatorId;
            set => _creatorId = value;
        }

        DateTime? IWithCreateDateBuilder.CreateDate
        {
            get => _createDate;
            set => _createDate = value;
        }

        DateTime? IWithUpdateDateBuilder.UpdateDate
        {
            get => _updateDate;
            set => _updateDate = value;
        }

        string IWithNameBuilder.Name
        {
            get => _name;
            set => _name = value;
        }

        bool? IWithTrashedBuilder.Trashed
        {
            get => _trashed;
            set => _trashed = value;
        }

        int? IWithLevelBuilder.Level
        {
            get => _level;
            set => _level = value;
        }

        string IWithPathBuilder.Path
        {
            get => _path;
            set => _path = value;
        }

        int? IWithSortOrderBuilder.SortOrder
        {
            get => _sortOrder;
            set => _sortOrder = value;
        }
        int? IWithParentIdBuilder.ParentId
        {
            get => _parentId;
            set => _parentId = value;
        }
    }
}
