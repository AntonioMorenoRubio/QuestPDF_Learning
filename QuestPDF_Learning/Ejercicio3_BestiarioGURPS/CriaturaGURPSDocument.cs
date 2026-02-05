using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDF_Learning.Ejercicio3_BestiarioGURPS
{
    public class CriaturaGURPSDocument : IDocument
    {
        public CriaturaGURPS Criatura { get; }

        public CriaturaGURPSDocument(CriaturaGURPS criatura)
        {
            Criatura = criatura;
        }

        public DocumentMetadata GetMetadata() => AssignMetadata();

        private DocumentMetadata AssignMetadata()
        {
            return new DocumentMetadata
            {
                Title = "Bestiario Personal para GURPS",
                Author = "Antonio Moreno Rubio",
                Keywords = "bestiario, gurps, monstruos",
                Language = "es-ES",
                CreationDate = DateTimeOffset.Now,
                ModifiedDate = DateTimeOffset.Now
            };
        }

        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(40);
                page.DefaultTextStyle(x => x.FontFamily("Segoe UI"));
                page.Content().Element(ComposeContent);
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(mainColumn =>
            {
                // HEADER - Nombre y Tipo
                mainColumn.Item()
                    .Background("#2c5f2d")
                    .Padding(10)
                    .Column(header =>
                    {
                        header.Item()
                            .Text(Criatura.Nombre)
                            .FontSize(20)
                            .Bold()
                            .FontColor(Colors.White);

                        if (!string.IsNullOrWhiteSpace(Criatura.Tipo))
                        {
                            header.Item()
                                .Text(Criatura.Tipo)
                                .FontSize(12)
                                .Italic()
                                .FontColor(Colors.White);
                        }
                    });

                // DESCRIPCIÓN
                if (!string.IsNullOrWhiteSpace(Criatura.Descripcion))
                {
                    mainColumn.Item()
                        .PaddingTop(15)
                        .Text(Criatura.Descripcion)
                        .FontSize(10)
                        .Italic();
                }

                // Dos columnas principales
                mainColumn.Item()
                    .PaddingTop(10)
                    .Row(row =>
                    {
                        // COLUMNA IZQUIERDA - Info general
                        row.RelativeItem()
                            .Column(leftCol =>
                            {
                                // Hábitat
                                if (Criatura.Habitat != null && Criatura.Habitat.Any())
                                {
                                    leftCol.Item()
                                        .PaddingBottom(10)
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("HÁBITAT")
                                                .FontSize(11)
                                                .Bold();

                                            foreach (var habitat in Criatura.Habitat)
                                            {
                                                col.Item()
                                                    .Text($"• {habitat}")
                                                    .FontSize(9);
                                            }
                                        });
                                }

                                // Comportamiento
                                if (!string.IsNullOrEmpty(Criatura.Comportamiento))
                                {
                                    leftCol.Item()
                                        .PaddingBottom(10)
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("COMPORTAMIENTO")
                                                .FontSize(11)
                                                .Bold();

                                            col.Item()
                                                .Text(Criatura.Comportamiento)
                                                .FontSize(9);
                                        });
                                }

                                // Entrenamiento
                                if (!string.IsNullOrEmpty(Criatura.Entrenamiento))
                                {
                                    leftCol.Item()
                                        .PaddingBottom(10)
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("ENTRENAMIENTO")
                                                .FontSize(11)
                                                .Bold();

                                            col.Item()
                                                .Text(Criatura.Entrenamiento)
                                                .FontSize(9);
                                        });
                                }

                                // Notas especiales
                                if (!string.IsNullOrEmpty(Criatura.NotasEspeciales))
                                {
                                    leftCol.Item()
                                        .PaddingBottom(10)
                                        .Column(col =>
                                        {
                                            col.Item()
                                                .Text("NOTAS ESPECIALES")
                                                .FontSize(11)
                                                .Bold();

                                            col.Item()
                                                .Text(Criatura.NotasEspeciales)
                                                .FontSize(9);
                                        });
                                }
                            });

                        row.ConstantItem(15); // Espacio entre columnas

                        // COLUMNA DERECHA - Ficha de combate
                        row.RelativeItem()
                            .Border(2)
                            .BorderColor("#2c5f2d")
                            .Background("#f8f6f0")
                            .Padding(10)
                            .Column(statBlock =>
                            {
                                statBlock.Item()
                                    .Text("FICHA DE COMBATE")
                                    .FontSize(12)
                                    .Bold();

                                statBlock.Item().PaddingTop(5).LineHorizontal(1).LineColor("#2c5f2d");

                                // ATRIBUTOS (dentro del statBlock)
                                if (Criatura.Atributos != null)
                                {
                                    statBlock.Item()
                                        .PaddingTop(8)
                                        .Column(attrs =>
                                        {
                                            attrs.Item()
                                                .Text("Atributos")
                                                .FontSize(10)
                                                .Bold();

                                            // Primera fila: ST, DX, IQ, HT
                                            attrs.Item().Row(row1 =>
                                            {
                                                row1.RelativeItem().Text(text =>
                                                {
                                                    text.Span("ST ").FontSize(9).Bold();
                                                    text.Span($"{Criatura.Atributos.ST}").FontSize(9);
                                                });
                                                row1.RelativeItem().Text(text =>
                                                {
                                                    text.Span("DX ").FontSize(9).Bold();
                                                    text.Span($"{Criatura.Atributos.DX}").FontSize(9);
                                                });
                                                row1.RelativeItem().Text(text =>
                                                {
                                                    text.Span("IQ ").FontSize(9).Bold();
                                                    text.Span($"{Criatura.Atributos.IQ}").FontSize(9);
                                                });
                                                row1.RelativeItem().Text(text =>
                                                {
                                                    text.Span("HT ").FontSize(9).Bold();
                                                    text.Span($"{Criatura.Atributos.HT}").FontSize(9);
                                                });
                                            });

                                            // Segunda fila: HP, FP, SM
                                            attrs.Item().Row(row2 =>
                                            {
                                                row2.RelativeItem().Text(text =>
                                                {
                                                    text.Span("HP ").FontSize(9).Bold();
                                                    text.Span($"{Criatura.Atributos.HP}").FontSize(9);
                                                });
                                                row2.RelativeItem().Text(text =>
                                                {
                                                    text.Span("FP ").FontSize(9).Bold();
                                                    text.Span($"{Criatura.Atributos.FP}").FontSize(9);
                                                });

                                                if (!string.IsNullOrEmpty(Criatura.Atributos.SizeModifier))
                                                {
                                                    row2.RelativeItem(2).Text(text => // RelativeItem(2) para darle más espacio
                                                    {
                                                        text.Span("SM: ").FontSize(9).Bold();
                                                        text.Span(Criatura.Atributos.SizeModifier).FontSize(9);
                                                    });
                                                }
                                                else
                                                {
                                                    row2.RelativeItem(2); // Espacio vacío si no hay SM
                                                }
                                            });
                                        });
                                }

                                // MOVIMIENTO
                                if (Criatura.Movimiento != null)
                                {
                                    statBlock.Item()
                                        .PaddingTop(8)
                                        .Column(mov =>
                                        {
                                            mov.Item()
                                                .Text("Movimiento")
                                                .FontSize(10)
                                                .Bold();

                                            mov.Item().Row(row =>
                                            {
                                                // Velocidad básica
                                                row.RelativeItem()
                                                .PaddingTop(3)
                                                .Text(text =>
                                                {
                                                    text.Span("Vel. básica: ").FontSize(9).Bold();
                                                    text.Span($"{Criatura.Movimiento.VelocidadBasica}").FontSize(9);
                                                });

                                                // Tipos de movimiento con números con fondo de color
                                                row.RelativeItem()
                                                .PaddingTop(3)
                                                .Text(text =>
                                                {
                                                    text.Span("Tierra ").Bold().FontSize(9);
                                                    text.Span($"{Criatura.Movimiento.MovimientoTierra}")
                                                        .FontSize(9)
                                                        .FontColor(Colors.Black);

                                                    // Separador
                                                    if (Criatura.Movimiento.MovimientoAgua.HasValue || Criatura.Movimiento.MovimientoAire.HasValue)
                                                    {
                                                        text.Span(" / ").FontSize(9);
                                                    }

                                                    // Movimiento Agua - solo si existe
                                                    if (Criatura.Movimiento.MovimientoAgua.HasValue)
                                                    {
                                                        text.Span("Agua ").Bold().FontSize(9);
                                                        text.Span($"{Criatura.Movimiento.MovimientoAgua.Value}")
                                                            .FontSize(9)
                                                            .FontColor(Colors.Black);

                                                        if (Criatura.Movimiento.MovimientoAire.HasValue)
                                                        {
                                                            text.Span(" / ").FontSize(9);
                                                        }
                                                    }

                                                    // Movimiento Aire - solo si existe
                                                    if (Criatura.Movimiento.MovimientoAire.HasValue)
                                                    {
                                                        text.Span("Aire ").Bold().FontSize(9);
                                                        text.Span($"{Criatura.Movimiento.MovimientoAire.Value}")
                                                            .FontSize(9)
                                                            .FontColor(Colors.Black);
                                                    }
                                                });
                                            });
                                        });
                                }

                                // DEFENSAS
                                if (Criatura.Defensas != null)
                                {
                                    statBlock.Item()
                                        .PaddingTop(8)
                                        .Column(def =>
                                        {
                                            def.Item()
                                                .Text("Defensas")
                                                .FontSize(10)
                                                .Bold();

                                            def.Item().Text(text =>
                                            {
                                                text.Span("Esquiva: ").FontSize(9).Bold();
                                                text.Span($"{Criatura.Defensas.Esquiva}").FontSize(9);
                                            });
                                        });
                                }

                                // ATAQUES
                                if (Criatura.Ataques != null && Criatura.Ataques.Any())
                                {
                                    statBlock.Item()
                                        .PaddingTop(8)
                                        .Column(atks =>
                                        {
                                            atks.Item()
                                                .Text("Ataques")
                                                .FontSize(10)
                                                .Bold();

                                            foreach (var ataque in Criatura.Ataques)
                                            {
                                                atks.Item()
                                                    .PaddingTop(3)
                                                    .Text(text =>
                                                    {
                                                        text.Span($"{ataque.Nombre} ({ataque.Habilidad}): ").FontSize(9).Bold();
                                                        text.Span($"{ataque.Dano}").FontSize(9);
                                                        if (!string.IsNullOrEmpty(ataque.Alcance))
                                                        {
                                                            text.Span($", {ataque.Alcance}").FontSize(9);
                                                        }
                                                    });
                                            }
                                        });
                                }

                                // ARMADURA
                                if (Criatura.Armadura != null)
                                {
                                    statBlock.Item()
                                        .PaddingTop(8)
                                        .Text(text =>
                                        {
                                            text.Span("DR: ").FontSize(9).Bold();
                                            text.Span(Criatura.Armadura.DR).FontSize(9);
                                        });
                                }

                                // RASGOS RELEVANTES
                                if (Criatura.RasgosRelevantes != null && Criatura.RasgosRelevantes.Any())
                                {
                                    statBlock.Item()
                                        .PaddingTop(8)
                                        .Column(rasgos =>
                                        {
                                            rasgos.Item()
                                                .Text("Rasgos")
                                                .FontSize(10)
                                                .Bold();

                                            rasgos.Item()
                                                .Text(string.Join(", ", Criatura.RasgosRelevantes))
                                                .FontSize(8);
                                        });
                                }

                                // HABILIDADES
                                if (Criatura.Habilidades != null && Criatura.Habilidades.Any())
                                {
                                    statBlock.Item()
                                        .PaddingTop(8)
                                        .Column(habs =>
                                        {
                                            habs.Item()
                                                .Text("Habilidades")
                                                .FontSize(10)
                                                .Bold();

                                            foreach (var hab in Criatura.Habilidades)
                                            {
                                                habs.Item()
                                                    .Text(text =>
                                                    {
                                                        text.Span($"{hab.Nombre}: ").FontSize(8).Bold();
                                                        text.Span(hab.Valor).FontSize(8);
                                                    });
                                            }
                                        });
                                }
                            });
                    });

                // TÁCTICA (abajo, ancho completo)
                if (!string.IsNullOrEmpty(Criatura.Tactica))
                {
                    mainColumn.Item()
                        .PaddingTop(15)
                        .Border(1)
                        .BorderColor("#2c5f2d")
                        .Background("#fafaf5")
                        .Padding(10)
                        .Column(col =>
                        {
                            col.Item()
                                .Text("TÁCTICA")
                                .FontSize(11)
                                .Bold();

                            col.Item()
                                .PaddingTop(3)
                                .Text(Criatura.Tactica)
                                .FontSize(9);
                        });
                }

                // VARIANTES (abajo, ancho completo)
                if (Criatura.Variantes != null && Criatura.Variantes.Any())
                {
                    mainColumn.Item()
                        .PaddingTop(10)
                        .Border(1)
                        .BorderColor("#2c5f2d")
                        .Background("#fafaf5")
                        .Padding(10)
                        .Column(col =>
                        {
                            col.Item()
                                .Text("VARIANTES")
                                .FontSize(11)
                                .Bold();

                            foreach (var variante in Criatura.Variantes)
                            {
                                col.Item()
                                    .PaddingTop(3)
                                    .Text(text =>
                                    {
                                        text.Span($"{variante.Nombre}: ").FontSize(9).Bold();

                                        // Mostrar modificadores estructurados
                                        if (variante.Modificadores.Any())
                                        {
                                            var modTextos = variante.Modificadores
                                                .Select(m => $"{m.Campo} {(m.Valor >= 0 ? "+" : "")}{m.Valor}");
                                            text.Span(string.Join(", ", modTextos)).FontSize(9);
                                        }
                                    });
                            }
                        });
                }
            });
        }
    }
}
