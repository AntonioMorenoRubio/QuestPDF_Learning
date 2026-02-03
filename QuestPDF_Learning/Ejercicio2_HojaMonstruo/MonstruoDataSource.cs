namespace QuestPDF_Learning.Ejercicio2_HojaMonstruo
{
    public static class MonstruoDataSource
    {
        public static MonstruoModel ObtenerAbaloth()
        {
            return new MonstruoModel
            {
                Nombre = "ABOLETH LASHER",
                Tipo = "Large aberrant magical beast (aquatic)",
                NivelDesafio = 17,
                PuntosGolpe = 700,
                DadosVida = "17d10+85 (700 hp)",

                Stats = new Estadisticas
                {
                    CA = 29,
                    Velocidad = 10,
                    Atributos = new Dictionary<string, int>
                    {
                        { "Str", 16 }, { "Dex", 16 }, { "Con", 25 },
                        { "Int", 23 }, { "Wis", 25 }, { "Cha", 22 }
                    },
                    Sentidos = "Senses Perception +14; darkvision",
                    Idiomas = "Languages Deep Speech, telepathy 20",
                    Habilidades = new List<string> { "Arcana +19", "Dungeoneering +20", "Insight +17" }
                },

                Habilidades = new List<Habilidad>
    {
        new Habilidad
        {
            Nombre = "Mucus Haze",
            Tipo = "aura 5",
            Descripcion = "enemies treat the area within the aura as difficult terrain."
        },
        new Habilidad
        {
            Nombre = "Slime Orb",
            Tipo = "standard-at-will ◆ Psychic",
            Descripcion = "Ranged 10; +22 vs. AC; 2d8 + 6 psychic damage, and the target is slowed (save ends)."
        }
    },

                Ataques = new List<Ataque>
    {
        new Ataque
        {
            Nombre = "Tentacle",
            TipoAccion = "standard-at-will",
            Rango = "Reach 2",
            Efecto = "+22 vs. AC; 2d8 + 8 damage (3d8 + 8 damage against a dazed target), and the target is dazed (save ends)."
        },
        new Ataque
        {
            Nombre = "Dominate Mind",
            TipoAccion = "standard-encounter ◆ Charm, Psychic",
            Rango = "Close burst 10",
            Efecto = "targets enemies; −20 vs. Will; 2d8 + 8 psychic damage, and the target is dazed (save ends)."
        }
    },

                Lore = "Few master knows the following information with a successful Dungeoneering check:\n" +
           "DC 20: Aboleths lair in the deepest reaches of the Underdark, in subterranean seas cut off from the sun. However, lone aboleths can be found closer to the world's surface, haunting ruins, deep lakes, and old temples without hope or want of companionship."
            };
        }


    }
}
