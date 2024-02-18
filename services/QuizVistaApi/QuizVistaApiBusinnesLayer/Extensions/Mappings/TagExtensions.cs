using Azure.Core;
using QuizVistaApiBusinnesLayer.Models.Requests;
using QuizVistaApiBusinnesLayer.Models.Responses;
using QuizVistaApiInfrastructureLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizVistaApiBusinnesLayer.Extensions.Mappings
{
    public static class TagExtensions
    {

        public static TagResponse ToResponse(this Tag tag)
        {
            if (tag is null) return null;

            return new TagResponse(
                tag.Id,
                tag.Name,
                tag.Quizzes.ToCollectionResponse().ToList()
            );
        }

        public static IEnumerable<TagResponse> ToCollectionResponse(this IEnumerable<Tag> tags)
        {
            if(tags is null || !tags.Any()) return Enumerable.Empty<TagResponse>();
            return tags.Select(ToResponse) ?? Enumerable.Empty<TagResponse>();
        }

        public static Tag ToEntity(this TagRequest tagRequest)
        {
            return new Tag
            {
                Id = tagRequest.Id,
                Name = tagRequest.Name
            };
        }

    }
}
