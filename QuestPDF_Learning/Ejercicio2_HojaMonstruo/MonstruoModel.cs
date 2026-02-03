namespace QuestPDF_Learning.Ejercicio2_HojaMonstruo
{
    public class MonstruoModel
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; } // "Large aberrant magical beast (aquatic)"
        public int NivelDesafio { get; set; }
        public int PuntosGolpe { get; set; }
        public string DadosVida { get; set; } // "17d10+85"

        public Estadisticas Stats { get; set; }
        public List<Habilidad> Habilidades { get; set; }
        public List<Ataque> Ataques { get; set; }
        public string Lore { get; set; }
    }

    public class Estadisticas
    {
        public int CA { get; set; } // Clase de Armadura
        public int Velocidad { get; set; }
        public Dictionary<string, int> Atributos { get; set; } // Str, Dex, Con, Int, Wis, Cha
        public string Sentidos { get; set; }
        public string Idiomas { get; set; }
        public List<string> Habilidades { get; set; } // Skills como "Arcana +19"
    }

    public class Habilidad
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; } // "Su" (sobrenatural), "Ps" (psíquico), etc.
        public string Descripcion { get; set; }
    }

    public class Ataque
    {
        public string Nombre { get; set; }
        public string TipoAccion { get; set; } // "standard", "swift", etc.
        public string Rango { get; set; } // "Reach 2", "Ranged 10", etc.
        public string Efecto { get; set; }
    }
}
