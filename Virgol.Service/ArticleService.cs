﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Virgol_Model;
using Virgol_Model.Context;

namespace Virgol.Service
{
    public class ArticleService : GenericService<Article>, IArticleService
    {
        public ArticleService(VirgolContext context) : base(context)
        {
        }
    }
}
