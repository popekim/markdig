﻿// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Textamina.Markdig.Renderers.Html;
using Textamina.Markdig.Syntax;
using Textamina.Markdig.Syntax.Inlines;

namespace Textamina.Markdig.Extensions.Bootstrap
{
    /// <summary>
    /// Extension for tagging some HTML elements with bootstrap classes.
    /// </summary>
    /// <seealso cref="Textamina.Markdig.IMarkdownExtension" />
    public class BootstrapExtension : IMarkdownExtension
    {
        public void Setup(MarkdownPipeline pipeline)
        {
            // Make sure we don't have a delegate twice
            pipeline.DocumentProcessed -= PipelineOnDocumentProcessed;
            pipeline.DocumentProcessed += PipelineOnDocumentProcessed;
        }

        private static void PipelineOnDocumentProcessed(MarkdownDocument document)
        {
            foreach(var node in document.Descendants())
            {
                if (node is Block)
                {
                    if (node is Tables.Table)
                    {
                        node.GetAttributes().AddClass("table");
                    }
                    else if (node is QuoteBlock)
                    {
                        node.GetAttributes().AddClass("blockquote");
                    }
                    else if (node is Figures.Figure)
                    {
                        node.GetAttributes().AddClass("figure");
                    }
                    else if (node is Figures.FigureCaption)
                    {
                        node.GetAttributes().AddClass("figure-caption");
                    }
                }
                else if (node is Inline)
                {
                    var link = node as LinkInline;
                    if (link != null && link.IsImage)
                    {
                        link.GetAttributes().AddClass("img-fluid");
                    }
                }
            }
        }
    }
}