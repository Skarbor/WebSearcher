using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebSearcher.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace WebSearcher.DataAccess.Context
{
    public class WebPageContext : BaseEntityContext<WebPage>
    {
        protected override string EntityName { get => "WebPages"; }
    }
}
