using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using PracticaBlazor.UI.Client;
using PracticaBlazor.UI.Client.Shared;
using Blazored.LocalStorage;
using Blazored.Toast;
using Blazored.Toast.Services;
using PracticaBlazor.UI.Shared.Models;
using System.Security.Claims;
using System.Text.Json;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Grid;
using Syncfusion.Drawing;
using Syncfusion.Pdf.Graphics;
using System.IO;
using System.Data;

namespace PracticaBlazor.UI.Client.Pages.CarritoCompra
{
    public partial class MostrarCarrito
    {
        private List<Producto> _carritosProd = new();
        private List<Carrito> _carritosUser = new();
        private int precioTotal = 0;
        [CascadingParameter]
        Task<AuthenticationState> authenticationStateTask { get; set; }
        AuthenticationState authState;

        private string userId;
        protected override async Task OnInitializedAsync()
        {
            //GET user Id
            authState = await authenticationStateTask;
            userId = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (userId != null)
            {
                _carritosUser = await CarroService.GetCarritoUser(Convert.ToInt32(userId));
                _carritosProd = await CarroService.GetCarritoProd(Convert.ToInt32(userId));
            }
            precioTotal = await CarroService.CalcularPrecioCarrito(_carritosProd, _carritosUser);
        }

        public async Task DeleteProdCarrito(int idCarrito)
        {

            await CarroService.BorrarCarrito(idCarrito, authState);
            _carritosUser = await CarroService.GetCarritoUser(Convert.ToInt32(userId));
            _carritosProd = await CarroService.GetCarritoProd(Convert.ToInt32(userId));
            precioTotal = await CarroService.CalcularPrecioCarrito(_carritosProd, _carritosUser);
            StateHasChanged();
        }

        void ExportToPdf()
        {
            int paragraphAfterSpacing = 8;
            int cellMargin = 8;
            PdfDocument pdfDocument = new PdfDocument();
            //Add Page to the PDF document.
            PdfPage page = pdfDocument.Pages.Add();
            //Create a new font.
            PdfStandardFont font = new PdfStandardFont(PdfFontFamily.TimesRoman, 16);
            //Create a text element to draw a text in PDF page.
            PdfTextElement title = new PdfTextElement("Factura pedido", font, PdfBrushes.Black);
            PdfLayoutResult result = title.Draw(page, new PointF(0, 0));
            PdfStandardFont contentFont = new PdfStandardFont(PdfFontFamily.TimesRoman, 12);
            PdfTextElement content = new PdfTextElement("Aquí muestro una prueba de una factura generada en pdf.", contentFont, PdfBrushes.Black);
            PdfLayoutFormat format = new PdfLayoutFormat();
            format.Layout = PdfLayoutType.Paginate;
            //Draw a text to the PDF document.
            result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);
            //Create a PdfGrid.
            PdfGrid pdfGrid = new PdfGrid();
            pdfGrid.Style.CellPadding.Left = cellMargin;
            pdfGrid.Style.CellPadding.Right = cellMargin;
            //Create a DataTable.
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("ID");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("Precio");
            foreach (var producto in _carritosProd)
            {
                precioTotal += producto.Precio;
                dataTable.Rows.Add(new object[]{producto.Id, producto.Nombre, producto.Precio});
            }

            //Assign data source.
            pdfGrid.DataSource = dataTable;
            //Create PDF grid build style settings instance.
            PdfGridBuiltinStyleSettings settings = new PdfGridBuiltinStyleSettings();
            settings.ApplyStyleForBandedColumns = true;
            settings.ApplyStyleForBandedRows = true;
            settings.ApplyStyleForFirstColumn = true;
            settings.ApplyStyleForHeaderRow = true;
            settings.ApplyStyleForLastColumn = true;
            settings.ApplyStyleForLastRow = true;
            //Apply built-in table style
            pdfGrid.ApplyBuiltinStyle(PdfGridBuiltinStyle.GridTable3, settings);
            //Draw grid to the page of PDF document.
            result = pdfGrid.Draw(page, new Syncfusion.Drawing.PointF(0, result.Bounds.Bottom + paragraphAfterSpacing));
            content = new PdfTextElement($"Precio{Environment.NewLine} Total = {precioTotal} €", contentFont, PdfBrushes.Black);
            //Draw a text to the PDF document.
            result = content.Draw(page, new RectangleF(0, result.Bounds.Bottom + paragraphAfterSpacing, page.GetClientSize().Width, page.GetClientSize().Height), format);
            MemoryStream memoryStream = new MemoryStream();
            // Save the PDF document.
            pdfDocument.Save(memoryStream);
            // Download the PDF document
            JS.SaveAs("Factura.pdf", memoryStream.ToArray());
        }



    }
}