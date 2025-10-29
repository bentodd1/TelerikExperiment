using Telerik.Reporting;
using Telerik.Reporting.Drawing;
using System.Drawing;

namespace TestReport1
{
    /// <summary>
    /// Vehicle Inventory Report - Q3 2025
    /// </summary>
    public partial class VehicleInventoryReport : Telerik.Reporting.Report
    {
        public VehicleInventoryReport()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.PageSettings.Landscape = true;
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Width = Telerik.Reporting.Drawing.Unit.Inch(10.5);

            // Report Header
            var reportHeader = new Telerik.Reporting.ReportHeaderSection();
            reportHeader.Height = Telerik.Reporting.Drawing.Unit.Inch(1.2);

            var titleTextBox = new Telerik.Reporting.TextBox();
            titleTextBox.Value = "Vehicle Inventory Report";
            titleTextBox.Style.Font.Bold = true;
            titleTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(18);
            titleTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            titleTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.1));
            titleTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(10.5), Telerik.Reporting.Drawing.Unit.Inch(0.4));
            reportHeader.Items.Add(titleTextBox);

            var quarterTextBox = new Telerik.Reporting.TextBox();
            quarterTextBox.Value = "Q3 2025";
            quarterTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(14);
            quarterTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            quarterTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.5));
            quarterTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(10.5), Telerik.Reporting.Drawing.Unit.Inch(0.3));
            reportHeader.Items.Add(quarterTextBox);

            var dateTextBox = new Telerik.Reporting.TextBox();
            dateTextBox.Value = "= 'Generated: ' + Now().ToString('MM/dd/yyyy')";
            dateTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            dateTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            dateTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.9));
            dateTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(10.5), Telerik.Reporting.Drawing.Unit.Inch(0.2));
            reportHeader.Items.Add(dateTextBox);

            this.Items.Add(reportHeader);

            // CSV Data Source
            var csvDataSource = new Telerik.Reporting.CsvDataSource();
            csvDataSource.Source = "vehicle_inventory.csv";
            csvDataSource.FieldSeparator = ",";
            csvDataSource.HasHeaders = true;

            // Detail Section with Table
            var detailSection = new Telerik.Reporting.DetailSection();
            detailSection.Height = Telerik.Reporting.Drawing.Unit.Inch(0.5);

            var table = new Telerik.Reporting.Table();
            table.DataSource = csvDataSource;
            table.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.2), Telerik.Reporting.Drawing.Unit.Inch(0));
            table.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(10), Telerik.Reporting.Drawing.Unit.Inch(0.5));

            // Table columns
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.6)));  // Year
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0)));  // Make
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.2)));  // Model
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0)));  // Trim
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.8)));  // Mileage
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.8)));  // Color
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0)));  // Purchase Price
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0)));  // List Price
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(1.0)));  // Margin
            table.Body.Columns.Add(new Telerik.Reporting.TableBodyColumn(Telerik.Reporting.Drawing.Unit.Inch(0.8)));  // Status

            table.Body.Rows.Add(new Telerik.Reporting.TableBodyRow(Telerik.Reporting.Drawing.Unit.Inch(0.25)));

            // Header Row
            table.Items.AddRange(CreateTableHeaders());

            // Data Row Cells
            table.Items.AddRange(CreateTableDataCells());

            // Apply styling
            ApplyTableStyling(table);

            detailSection.Items.Add(table);
            this.Items.Add(detailSection);

            // Report Footer with Summary
            var reportFooter = new Telerik.Reporting.ReportFooterSection();
            reportFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.8);

            var summaryTextBox = new Telerik.Reporting.TextBox();
            summaryTextBox.Value = "= 'Total Vehicles: ' + Count(Fields.VIN)";
            summaryTextBox.Style.Font.Bold = true;
            summaryTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            summaryTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.2), Telerik.Reporting.Drawing.Unit.Inch(0.2));
            summaryTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3), Telerik.Reporting.Drawing.Unit.Inch(0.25));
            reportFooter.Items.Add(summaryTextBox);

            var totalInvestmentTextBox = new Telerik.Reporting.TextBox();
            totalInvestmentTextBox.Value = "= 'Total Investment: ' + Format('{0:C}', Sum(CDbl(Fields.PurchasePrice)))";
            totalInvestmentTextBox.Style.Font.Bold = true;
            totalInvestmentTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            totalInvestmentTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0.2), Telerik.Reporting.Drawing.Unit.Inch(0.5));
            totalInvestmentTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3), Telerik.Reporting.Drawing.Unit.Inch(0.25));
            reportFooter.Items.Add(totalInvestmentTextBox);

            var totalListValueTextBox = new Telerik.Reporting.TextBox();
            totalListValueTextBox.Value = "= 'Total List Value: ' + Format('{0:C}', Sum(CDbl(Fields.ListPrice)))";
            totalListValueTextBox.Style.Font.Bold = true;
            totalListValueTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(10);
            totalListValueTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(3.5), Telerik.Reporting.Drawing.Unit.Inch(0.5));
            totalListValueTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(3), Telerik.Reporting.Drawing.Unit.Inch(0.25));
            reportFooter.Items.Add(totalListValueTextBox);

            this.Items.Add(reportFooter);

            // Page Footer
            var pageFooter = new Telerik.Reporting.PageFooterSection();
            pageFooter.Height = Telerik.Reporting.Drawing.Unit.Inch(0.4);

            var pageNumberTextBox = new Telerik.Reporting.TextBox();
            pageNumberTextBox.Value = "= 'Page ' + PageNumber + ' of ' + PageCount";
            pageNumberTextBox.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            pageNumberTextBox.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            pageNumberTextBox.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Inch(0), Telerik.Reporting.Drawing.Unit.Inch(0.1));
            pageNumberTextBox.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Inch(10.5), Telerik.Reporting.Drawing.Unit.Inch(0.2));
            pageFooter.Items.Add(pageNumberTextBox);

            this.Items.Add(pageFooter);
        }

        private Telerik.Reporting.ReportItem[] CreateTableHeaders()
        {
            var headers = new[]
            {
                CreateHeaderCell("Year", 0, 0),
                CreateHeaderCell("Make", 0, 1),
                CreateHeaderCell("Model", 0, 2),
                CreateHeaderCell("Trim", 0, 3),
                CreateHeaderCell("Mileage", 0, 4),
                CreateHeaderCell("Color", 0, 5),
                CreateHeaderCell("Purchase Price", 0, 6),
                CreateHeaderCell("List Price", 0, 7),
                CreateHeaderCell("Margin", 0, 8),
                CreateHeaderCell("Status", 0, 9)
            };

            return headers;
        }

        private Telerik.Reporting.TextBox CreateHeaderCell(string text, int row, int column)
        {
            var cell = new Telerik.Reporting.TextBox();
            cell.Value = text;
            cell.Style.BackgroundColor = System.Drawing.Color.FromArgb(51, 102, 153);
            cell.Style.Color = System.Drawing.Color.White;
            cell.Style.Font.Bold = true;
            cell.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(9);
            cell.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            cell.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            cell.Style.Padding.All = Telerik.Reporting.Drawing.Unit.Pixel(3);
            cell.RowIndex = row;
            cell.ColumnIndex = column;

            return cell;
        }

        private Telerik.Reporting.ReportItem[] CreateTableDataCells()
        {
            return new Telerik.Reporting.ReportItem[]
            {
                CreateDataCell("= Fields.Year", 1, 0, HorizontalAlign.Center),
                CreateDataCell("= Fields.Make", 1, 1, HorizontalAlign.Left),
                CreateDataCell("= Fields.Model", 1, 2, HorizontalAlign.Left),
                CreateDataCell("= Fields.Trim", 1, 3, HorizontalAlign.Left),
                CreateDataCell("= Format('{0:N0}', CDbl(Fields.Mileage))", 1, 4, HorizontalAlign.Right),
                CreateDataCell("= Fields.Color", 1, 5, HorizontalAlign.Left),
                CreateDataCell("= Format('{0:C}', CDbl(Fields.PurchasePrice))", 1, 6, HorizontalAlign.Right),
                CreateDataCell("= Format('{0:C}', CDbl(Fields.ListPrice))", 1, 7, HorizontalAlign.Right),
                CreateDataCell("= Format('{0:C}', CDbl(Fields.ListPrice) - CDbl(Fields.PurchasePrice))", 1, 8, HorizontalAlign.Right),
                CreateDataCell("= Fields.Status", 1, 9, HorizontalAlign.Center)
            };
        }

        private Telerik.Reporting.TextBox CreateDataCell(string value, int row, int column, HorizontalAlign align)
        {
            var cell = new Telerik.Reporting.TextBox();
            cell.Value = value;
            cell.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(8);
            cell.Style.TextAlign = align;
            cell.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            cell.Style.Padding.All = Telerik.Reporting.Drawing.Unit.Pixel(3);
            cell.RowIndex = row;
            cell.ColumnIndex = column;

            return cell;
        }

        private void ApplyTableStyling(Telerik.Reporting.Table table)
        {
            table.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            table.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Pixel(1);
            table.Style.BorderColor.Default = System.Drawing.Color.LightGray;
        }
    }
}
