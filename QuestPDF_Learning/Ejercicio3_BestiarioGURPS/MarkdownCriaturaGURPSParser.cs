using System.Text.RegularExpressions;

namespace QuestPDF_Learning.Ejercicio3_BestiarioGURPS
{
    public static class MarkdownCriaturaGURPSParser
    {
        public static CriaturaGURPS ParsearDesdeArchivo(string rutaArchivo)
        {
            string contenido = File.ReadAllText(rutaArchivo);
            return ParsearDesdeTexto(contenido);
        }

        private static string LimpiarValor(string valor)
        {
            return valor.Replace("*", "").Trim();
        }

        private static string ExtraerIdSeccion(string lineaEncabezado)
        {
            // Extrae {#id} o usa el título normalizado
            var match = Regex.Match(lineaEncabezado, @"(.+?)\s*\{#(.+?)\}");
            if (match.Success)
            {
                return match.Groups[2].Value.Trim().ToLowerInvariant();
            }

            // Fallback: normaliza el título
            string sinAlmohadillas = Regex.Replace(lineaEncabezado, @"^#+\s*", ""); // Quita todos los # del inicio

            return sinAlmohadillas
                .Trim()
                .ToLowerInvariant()
                .Replace("á", "a").Replace("é", "e").Replace("í", "i")
                .Replace("ó", "o").Replace("ú", "u").Replace("ñ", "n")
                .Replace(" ", "");
        }

        public static CriaturaGURPS ParsearDesdeTexto(string markdown)
        {
            var criatura = new CriaturaGURPS
            {
                Habitat = new List<string>(),
                Ataques = new List<AtaqueGURPS>(),
                RasgosRelevantes = new List<string>(),
                Habilidades = new List<HabilidadGURPS>(),
                Variantes = new List<VarianteGURPS>()
            };

            var lineas = markdown.Split('\n').Select(l => l.Trim()).ToArray();

            string seccionActual = "";

            for (int i = 0; i < lineas.Length; i++)
            {
                var linea = lineas[i];

                if (string.IsNullOrWhiteSpace(linea)) continue;

                // Nombre (# Grifo)
                if (linea.StartsWith("# ") && string.IsNullOrEmpty(criatura.Nombre))
                {
                    criatura.Nombre = linea.Substring(2).Trim();
                    // Siguiente línea suele ser el tipo
                    if (i + 1 < lineas.Length && !lineas[i + 1].StartsWith("#"))
                    {
                        criatura.Tipo = lineas[i + 1].Trim();
                        i++; // Saltar siguiente línea
                    }
                    continue;
                }

                // Secciones principales (##) y Subsecciones (###)
                if (linea.StartsWith("## ") || linea.StartsWith("### "))
                {
                    seccionActual = ExtraerIdSeccion(linea);
                    continue;
                }

                // Procesar contenido según sección
                switch (seccionActual)
                {
                    case "descripcion":
                        criatura.Descripcion = (criatura.Descripcion ?? "") + linea + " ";
                        break;

                    case "habitat":
                        if (linea.StartsWith("- "))
                            criatura.Habitat.Add(linea.Substring(2).Trim());
                        break;

                    case "comportamiento":
                        criatura.Comportamiento = (criatura.Comportamiento ?? "") + linea + " ";
                        break;

                    case "entrenamiento":
                        criatura.Entrenamiento = (criatura.Entrenamiento ?? "") + linea + " ";
                        break;

                    case "notasespeciales":
                        criatura.NotasEspeciales = (criatura.NotasEspeciales ?? "") + linea + " ";
                        break;

                    case "atributos":
                        ParsearAtributo(linea, criatura);
                        break;

                    case "movimiento":
                        ParsearMovimiento(linea, criatura);
                        break;

                    case "defensas":
                        if (linea.StartsWith("- **Esquiva:**"))
                        {
                            criatura.Defensas = new DefensasGURPS
                            {
                                Esquiva = ExtraerEnteroConSigno(linea)
                            };
                        }
                        break;

                    case "ataques":
                        if (linea.StartsWith("- **"))
                        {
                            var ataque = ParsearAtaque(linea);
                            if (ataque != null)
                                criatura.Ataques.Add(ataque);
                        }
                        break;

                    case "armadura":
                        if (linea.StartsWith("- **DR:**"))
                        {
                            criatura.Armadura = new ArmaduraGURPS
                            {
                                DR = LimpiarValor(linea.Split(':')[1]) // CAMBIO AQUÍ
                            };
                        }
                        break;

                    case "rasgosrelevantes":
                        if (linea.StartsWith("- "))
                            criatura.RasgosRelevantes.Add(linea.Substring(2).Trim());
                        break;

                    case "habilidades":
                        if (linea.StartsWith("- **"))
                        {
                            var habilidad = ParsearHabilidad(linea);
                            if (habilidad != null)
                                criatura.Habilidades.Add(habilidad);
                        }
                        break;

                    case "tactica":
                        criatura.Tactica = (criatura.Tactica ?? "") + linea + " ";
                        break;

                    case "variantesrapidas":
                        if (linea.StartsWith("- **"))
                        {
                            var variante = ParsearVariante(linea);
                            if (variante != null)
                                criatura.Variantes.Add(variante);
                        }
                        break;
                }
            }

            // Limpiar strings con espacios extra
            criatura.Descripcion = criatura.Descripcion?.Trim();
            criatura.Comportamiento = criatura.Comportamiento?.Trim();
            criatura.Entrenamiento = criatura.Entrenamiento?.Trim();
            criatura.NotasEspeciales = criatura.NotasEspeciales?.Trim();
            criatura.Tactica = criatura.Tactica?.Trim();

            return criatura;
        }

        private static void ParsearAtributo(string linea, CriaturaGURPS criatura)
        {
            if (criatura.Atributos == null)
                criatura.Atributos = new AtributosGURPS();

            if (linea.StartsWith("- **ST:**"))
                criatura.Atributos.ST = ExtraerEnteroConSigno(linea);
            else if (linea.StartsWith("- **DX:**"))
                criatura.Atributos.DX = ExtraerEnteroConSigno(linea);
            else if (linea.StartsWith("- **IQ:**"))
                criatura.Atributos.IQ = ExtraerEnteroConSigno(linea);
            else if (linea.StartsWith("- **HT:**"))
                criatura.Atributos.HT = ExtraerEnteroConSigno(linea);
            else if (linea.StartsWith("- **HP:**"))
                criatura.Atributos.HP = ExtraerEnteroConSigno(linea);
            else if (linea.StartsWith("- **FP:**"))
                criatura.Atributos.FP = ExtraerEnteroConSigno(linea);
            else if (linea.StartsWith("- **SM:**"))
                criatura.Atributos.SizeModifier = LimpiarValor(linea.Split(':')[1]); // CAMBIO AQUÍ
        }

        private static void ParsearMovimiento(string linea, CriaturaGURPS criatura)
        {
            if (criatura.Movimiento == null)
                criatura.Movimiento = new MovimientoGURPS();

            if (linea.StartsWith("- **Velocidad básica:**"))
            {
                var valor = linea.Split(':')[1].Trim();
                // Limpiar asteriscos y parsear con cultura invariante (punto decimal)
                valor = valor.Replace("*", "").Trim();
                criatura.Movimiento.VelocidadBasica = decimal.Parse(valor, System.Globalization.CultureInfo.InvariantCulture);
            }
            else if (linea.StartsWith("- **Movimiento (Tierra):**"))
            {
                criatura.Movimiento.MovimientoTierra = ExtraerEnteroConSigno(linea);
            }
            else if (linea.StartsWith("- **Movimiento (Agua):**"))
            {
                criatura.Movimiento.MovimientoAgua = ExtraerEnteroConSigno(linea);
            }
            else if (linea.StartsWith("- **Movimiento (Aire):**"))
            {
                criatura.Movimiento.MovimientoAire = ExtraerEnteroConSigno(linea);
            }
        }

        private static AtaqueGURPS ParsearAtaque(string linea)
        {
            // Formato: - **Pico (14):** 1d+1 pi+, Alcance C
            var match = Regex.Match(linea, @"- \*\*(.+?)\s*\((\d+)\):\*\*\s*(.+)");
            if (!match.Success) return null;

            string nombre = match.Groups[1].Value.Trim();
            int habilidad = int.Parse(match.Groups[2].Value);
            string resto = match.Groups[3].Value.Trim();

            // Separar daño y alcance
            var partes = resto.Split(',');
            string dano = partes[0].Trim();
            string alcance = partes.Length > 1 ? partes[1].Replace("Alcance", "").Trim() : "";

            return new AtaqueGURPS
            {
                Nombre = nombre,
                Habilidad = habilidad,
                Dano = dano,
                Alcance = alcance
            };
        }

        private static HabilidadGURPS ParsearHabilidad(string linea)
        {
            // Formato: - **Brawling:** 14
            var match = Regex.Match(linea, @"- \*\*(.+?):\*\*\s*(.+)");
            if (!match.Success) return null;

            return new HabilidadGURPS
            {
                Nombre = match.Groups[1].Value.Trim(),
                Valor = match.Groups[2].Value.Trim()
            };
        }

        private static VarianteGURPS ParsearVariante(string linea)
        {
            // Formato: - **Cría:** ST -4, DR -2
            var match = Regex.Match(linea, @"- \*\*(.+?):\*\*\s*(.+)");
            if (!match.Success) return null;

            var variante = new VarianteGURPS
            {
                Nombre = match.Groups[1].Value.Trim()
            };

            // Parsear modificadores: "ST -4, DR -2"
            var modTexto = match.Groups[2].Value.Trim();
            var partes = modTexto.Split(',');

            foreach (var parte in partes)
            {
                var modMatch = Regex.Match(parte.Trim(), @"(\w+)\s*([-+]?\d+)");
                if (modMatch.Success)
                {
                    variante.Modificadores.Add(new ModificadorAtributo
                    {
                        Campo = modMatch.Groups[1].Value.Trim(),
                        Valor = int.Parse(modMatch.Groups[2].Value)
                    });
                }
            }

            return variante;
        }

        private static int ExtraerEnteroConSigno(string texto)
        {
            var match = Regex.Match(texto, @"[-+]?\d+");
            return match.Success ? int.Parse(match.Value) : 0;
        }
    }
}
