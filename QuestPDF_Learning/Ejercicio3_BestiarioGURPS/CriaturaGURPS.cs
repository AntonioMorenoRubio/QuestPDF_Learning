namespace QuestPDF_Learning.Ejercicio3_BestiarioGURPS
{
    public class CriaturaGURPS
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; } // "Animal (Híbrido)"
        public string Descripcion { get; set; }
        public List<string> Habitat { get; set; }
        public string Comportamiento { get; set; }
        public string Entrenamiento { get; set; }
        public string NotasEspeciales { get; set; }

        // Ficha de Combate
        public AtributosGURPS Atributos { get; set; }
        public MovimientoGURPS Movimiento { get; set; }
        public DefensasGURPS Defensas { get; set; }
        public List<AtaqueGURPS> Ataques { get; set; }
        public ArmaduraGURPS Armadura { get; set; }
        public List<string> RasgosRelevantes { get; set; }
        public List<HabilidadGURPS> Habilidades { get; set; }
        public string Tactica { get; set; }
        public List<VarianteGURPS> Variantes { get; set; }
    }

    public class AtributosGURPS
    {
        public int ST { get; set; }
        public int DX { get; set; }
        public int IQ { get; set; }
        public int HT { get; set; }
        public int HP { get; set; }
        public int FP { get; set; }
        public string SizeModifier { get; set; } // "+1 (2 hexes)"
    }

    public class MovimientoGURPS
    {
        public decimal VelocidadBasica { get; set; }
        public int MovimientoTierra { get; set; }
        public int? MovimientoAgua { get; set; } // Nullable, no todos nadan
        public int? MovimientoAire { get; set; } // Nullable, no todos vuelan
    }

    public class DefensasGURPS
    {
        public int Esquiva { get; set; }
    }

    public class AtaqueGURPS
    {
        public string Nombre { get; set; } // "Pico"
        public int Habilidad { get; set; } // 14
        public string Dano { get; set; } // "1d+1 pi+"
        public string Alcance { get; set; } // "C"
    }

    public class ArmaduraGURPS
    {
        public string DR { get; set; } // "2 (total)" o puede ser más complejo
    }

    public class HabilidadGURPS
    {
        public string Nombre { get; set; }
        public string Valor { get; set; } // "14" o "15 (si entrenado)"
    }

    public class ModificadorAtributo
    {
        public string Campo { get; set; }  // "ST", "DR", "HP"
        public int Valor { get; set; }     // -4, +2

        public override string ToString() => $"{Campo} {(Valor >= 0 ? "+" : "")}{Valor}";
    }

    public class VarianteGURPS
    {
        public string Nombre { get; set; }
        public List<ModificadorAtributo> Modificadores { get; set; } = new();

        // Para backward compatibility
        public string ModificadoresTexto => string.Join(", ", Modificadores);
    }
}