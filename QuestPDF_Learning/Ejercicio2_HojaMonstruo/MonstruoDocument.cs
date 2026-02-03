using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDF_Learning.Ejercicio2_HojaMonstruo
{
    public class MonstruoDocument : IDocument
    {
        public MonstruoModel Monstruo { get; }

        public MonstruoDocument(MonstruoModel monstruo)
        {
            Monstruo = monstruo;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        public DocumentSettings GetSettings() => DocumentSettings.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);

                page.MarginHorizontal(50);
                page.MarginVertical(20);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {

        }

        private void ComposeContent(IContainer container)
        {
            container
                .MultiColumn(multicol =>
                {
                    multicol.Columns(2);
                    multicol.Spacing(50);

                    multicol.Content()
                    .Column(col =>
                    {
                        col.Item()
                            .Background("#3d5a3d")
                            .Padding(8)
                            .Text(Monstruo.Nombre)
                            .FontSize(18)
                            .Bold()
                            .FontColor(Colors.White);

                        // Texto descriptivo (flavor text)
                        col.Item()
                            .PaddingVertical(10)
                            .Text("Aboleths are hulking amphibious creatures that hail from the Far Realm " +
                            "a distant and unfathomable plane. They live in the Underdark, " +
                            "swimming through drowned canyons or creeping through lightless tunnels " +
                            "and leaving trails of glistening slime in their wake. Cruel and evil, " +
                            "aboleths bend humanoid creatures to their will, and more powerful aboleths " +
                            "can transform their minions into slimy horrors.")
                            .FontSize(9)
                            .Italic();

                        col.Item()
                            .Border(2)
                            .BorderColor("#3d5a3d")
                            .Background("#f5f0e8")
                            .Padding(10)
                            .Column(statBlock =>
                            {
                                statBlock.Item()
                                    .BorderBottom(1)
                                    .BorderColor("#8b7355")
                                    .PaddingBottom(5)
                                    .Row(titleRow =>
                                    {
                                        titleRow.RelativeItem()
                                            .Text("Aboleth Lasher")
                                            .FontSize(11)
                                            .Bold();

                                        titleRow.AutoItem()
                                            .Text($"Level {Monstruo.NivelDesafio} Brute")
                                            .FontSize(9)
                                            .Bold();
                                    });

                                statBlock.Item()
                                .PaddingTop(5)
                                .Text(Monstruo.Tipo)
                                .FontSize(8)
                                .Italic();

                                // Initiative / Senses / HP / Bloodied
                                statBlock.Item()
                                    .PaddingTop(8)
                                    .Text(text =>
                                    {
                                        text.Span("Initiative").FontSize(8).Bold();
                                        text.Span(" +13 ").FontSize(8);
                                        text.Span("Senses ").FontSize(8).Bold();
                                        text.Span($"{Monstruo.Stats.Sentidos}").FontSize(8);
                                    });

                                statBlock.Item()
                                    .Text(text =>
                                    {
                                        text.Span("HP ").FontSize(8).Bold();
                                        text.Span($"{Monstruo.PuntosGolpe} ").FontSize(8);
                                        text.Span("Bloodied ").FontSize(8).Bold();
                                        text.Span($"{Monstruo.PuntosGolpe / 2}").FontSize(8);
                                    });

                                // Habilidades especiales (aura, etc.)
                                foreach (var habilidad in Monstruo.Habilidades)
                                {
                                    statBlock.Item()
                                        .PaddingTop(6)
                                        .Text(text =>
                                        {
                                            text.Span($"⚡ {habilidad.Nombre} ").FontSize(8).Bold();
                                            text.Span($"({habilidad.Tipo})").FontSize(8).Italic();
                                            text.Span($"\n{habilidad.Descripcion}").FontSize(8);
                                        });
                                }

                                // AC / Fort / Ref / Will
                                statBlock.Item()
                                    .PaddingTop(8)
                                    .Text(text =>
                                    {
                                        text.Span($"AC {Monstruo.Stats.CA}; ").FontSize(8).Bold();
                                        text.Span("Fortitude 23, Reflex 25, Will 23").FontSize(8);
                                    });

                                // Speed
                                statBlock.Item()
                                    .Text($"Speed {Monstruo.Stats.Velocidad}, swim 10")
                                    .FontSize(8)
                                    .Bold();

                                // Ataques
                                statBlock.Item()
                                    .PaddingTop(6)
                                    .Text("Action Points 1")
                                    .FontSize(8)
                                    .Bold();

                                foreach (var ataque in Monstruo.Ataques)
                                {
                                    statBlock.Item()
                                        .PaddingTop(6)
                                        .Text(text =>
                                        {
                                            // Símbolo según tipo de acción
                                            string simbolo = ataque.TipoAccion.Contains("encounter") ? "⚔" : "⚔";

                                            text.Span($"{simbolo} {ataque.Nombre} ").FontSize(8).Bold();
                                            text.Span($"({ataque.TipoAccion})").FontSize(7).Italic();
                                            if (!string.IsNullOrEmpty(ataque.Rango))
                                            {
                                                text.Span($"\n{ataque.Rango}; ").FontSize(8);
                                            }
                                            text.Span($"{ataque.Efecto}").FontSize(8);
                                        });
                                }

                                // Alignment / Languages / Skills
                                statBlock.Item()
                                    .PaddingTop(8)
                                    .BorderTop(1)
                                    .BorderColor("#8b7355")
                                    .PaddingTop(6)
                                    .Row(bottomRow =>
                                    {
                                        bottomRow.RelativeItem()
                                            .Column(col =>
                                            {
                                                col.Item().Text(text =>
                                                {
                                                    text.Span("Alignment ").FontSize(7).Bold();
                                                    text.Span("Evil").FontSize(7);
                                                });

                                                col.Item().Text(text =>
                                                {
                                                    text.Span("Skills ").FontSize(7).Bold();
                                                    text.Span(string.Join(", ", Monstruo.Stats.Habilidades)).FontSize(7);
                                                });
                                            });

                                        bottomRow.RelativeItem()
                                            .Column(col =>
                                            {
                                                col.Item().Text(text =>
                                                {
                                                    text.Span("Languages ").FontSize(7).Bold();
                                                    text.Span(Monstruo.Stats.Idiomas).FontSize(7);
                                                });
                                            });
                                    });

                                // Atributos (Str, Dex, etc.) - 2 filas de 3 columnas
                                statBlock.Item()
                                    .PaddingTop(6)
                                    .Column(attrColumn =>
                                    {
                                        // Primera fila: STR, DEX, CON
                                        attrColumn.Item().Row(row1 =>
                                        {
                                            row1.RelativeItem().Text(text =>
                                            {
                                                text.Span("Str ").FontSize(7).Bold();
                                                text.Span($"{Monstruo.Stats.Atributos["Str"]} (+{(Monstruo.Stats.Atributos["Str"] - 10) / 2})").FontSize(7);
                                            });

                                            row1.RelativeItem().Text(text =>
                                            {
                                                text.Span("Dex ").FontSize(7).Bold();
                                                text.Span($"{Monstruo.Stats.Atributos["Dex"]} (+{(Monstruo.Stats.Atributos["Dex"] - 10) / 2})").FontSize(7);
                                            });

                                            row1.RelativeItem().Text(text =>
                                            {
                                                text.Span("Con ").FontSize(7).Bold();
                                                text.Span($"{Monstruo.Stats.Atributos["Con"]} (+{(Monstruo.Stats.Atributos["Con"] - 10) / 2})").FontSize(7);
                                            });
                                        });

                                        // Segunda fila: INT, WIS, CHA
                                        attrColumn.Item().Row(row2 =>
                                        {
                                            row2.RelativeItem().Text(text =>
                                            {
                                                text.Span("Int ").FontSize(7).Bold();
                                                text.Span($"{Monstruo.Stats.Atributos["Int"]} (+{(Monstruo.Stats.Atributos["Int"] - 10) / 2})").FontSize(7);
                                            });

                                            row2.RelativeItem().Text(text =>
                                            {
                                                text.Span("Wis ").FontSize(7).Bold();
                                                text.Span($"{Monstruo.Stats.Atributos["Wis"]} (+{(Monstruo.Stats.Atributos["Wis"] - 10) / 2})").FontSize(7);
                                            });

                                            row2.RelativeItem().Text(text =>
                                            {
                                                text.Span("Cha ").FontSize(7).Bold();
                                                text.Span($"{Monstruo.Stats.Atributos["Cha"]} (+{(Monstruo.Stats.Atributos["Cha"] - 10) / 2})").FontSize(7);
                                            });
                                        });
                                    });
                            });

                        col.Item().PaddingTop(10);

                        // TACTICS (caja beige clara)
                        col.Item()
                            .Border(1)
                            .BorderColor("#8b7355")
                            .Background("#faf8f3")
                            .Padding(10)
                            .Column(tacticsBlock =>
                            {
                                tacticsBlock.Item()
                                    .Text("ABOLETH SLIME MAGE TACTICS")
                                    .FontSize(10)
                                    .Bold();

                                tacticsBlock.Item()
                                    .PaddingTop(6)
                                    .Text("An aboleth slime mage prefers to lure its undeeding fight into the water, where it dominates one foe and uses it to keep opponents away while it blasts them with ranged attacks. It uses slime orb and slime burst to slow its enemies' approach and tries to dominate one of them as soon as possible.")
                                    .FontSize(8);
                            });

                        col.Item().PaddingTop(10);

                        // LORE
                        col.Item()
                            .Border(1)
                            .BorderColor("#8b7355")
                            .Background("#faf8f3")
                            .Padding(10)
                            .Column(loreBlock =>
                            {
                                loreBlock.Item()
                                    .Text("ABOLETH LORE")
                                    .FontSize(10)
                                    .Bold();

                                loreBlock.Item()
                                    .PaddingTop(6)
                                    .Text(Monstruo.Lore)
                                    .FontSize(8);
                            });

                        col.Item()
                        .Image("F:\\Repositorios\\QuestPDF_Learning\\Resources\\aboleth.png")
                        .FitWidth();
                    });
                });
        }

        private void ComposeFooter(IContainer container)
        {

        }
    }
}
