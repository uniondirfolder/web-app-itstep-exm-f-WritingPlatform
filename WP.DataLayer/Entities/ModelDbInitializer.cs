

using System.Collections.Generic;
using System.Data.Entity;

namespace WP.DataLayer.Entities
{
    public class ModelDbInitializer: CreateDatabaseIfNotExists<ModelDb>
    {
        protected override void Seed(ModelDb context)
        {
            List<Genre> Genres = new List<Genre> {
                new Genre {Name = "Фэнтези" },
                new Genre {Name = "Фантастика" },
                new Genre {Name = "Детектив" },
                new Genre {Name = "Классическая литература" },
                new Genre {Name = "Ужасы" },
                new Genre {Name = "Приключения" },
                new Genre {Name = "Боевик" },
                new Genre {Name = "Психология" },
                new Genre {Name = "Культура и искусство" },
                new Genre {Name = "Красота и здоровье" },
                new Genre {Name = "Компьютерная литература" },
                new Genre {Name = "Исторический" },
                new Genre {Name = "Словари, справочники" },
                new Genre {Name = "Юмористическая литература" },
                new Genre {Name = "Наука и образование" }
            };
            context.Genres.AddRange(Genres);

            base.Seed(context);
        }
    }
}
