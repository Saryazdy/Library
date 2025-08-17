using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Library.Application.Common.Mappings
{
    public static class MappingExtensions
    {
        public static async Task<List<TDestination>> ProjectToListAsync<TDestination>(
            this IQueryable queryable, IConfigurationProvider configuration, CancellationToken ct = default)
            => await queryable.ProjectTo<TDestination>(configuration).ToListAsync(ct);
    }
}
