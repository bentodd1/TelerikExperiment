# Telerik Reports Project

This project contains Telerik Report definitions for vehicle inventory and other reporting needs.

## Report Files

- **VehicleInventoryReport.trdx** - Q3 2025 Vehicle Inventory Report with pricing and margin analysis
- **vehicle_inventory.csv** - Sample data source for vehicle inventory
- **ReconditioningCostReport.trdx** - Q3 2025 Reconditioning Cost Report with year-based analysis
  - Features bar chart grouped by vehicle year
  - Shows Total Actual vs Total Estimate costs side-by-side
  - Includes visible data point labels with currency formatting
  - Data source: reconditioning_costs.csv

## Telerik Report Schema Information

### Current Schema Version
This project uses: `http://schemas.telerik.com/reporting/2025/2.0`

### Finding Your Schema Version
To determine the correct XML schema version for your Telerik Report Designer installation:

1. Open Telerik Standalone Report Designer
2. Create a new blank report
3. Save it as a `.trdx` file
4. Open the `.trdx` file in a text editor
5. Look at the `xmlns` attribute in the root `<Report>` tag

Example:
```xml
<Report Width="9.6in" Name="Report3" xmlns="http://schemas.telerik.com/reporting/2025/2.0">
```

### Schema Version History

Telerik Reporting XML schema versions follow this pattern:

**Pre-2017 Versions:**
- 2012 Q1: `http://schemas.telerik.com/reporting/2012/1`
- 2012 Q2: `http://schemas.telerik.com/reporting/2012/2`
- 2012 Q3: `http://schemas.telerik.com/reporting/2012/3`
- 2013 Q1 - 2016 R3: Various incremental versions (3.1 through 4.2)

**2017 R1 Onwards:**
Format changed to: `http://schemas.telerik.com/reporting/[year]/[major].[minor]`

**Recent Versions:**
- 2025 Q1: `http://schemas.telerik.com/reporting/2025/1.0`
- 2025 Q2: `http://schemas.telerik.com/reporting/2025/2.0` (Current)

### File Formats

- **`.trdx`** - XML Report Definition (plain text XML file)
- **`.trdp`** - Packaged Report Definition (ZIP archive containing report and resources)
- **`.cs`** - C# Code-behind class (for programmatic report definitions)

## Report Structure Template

When creating new `.trdx` reports, use this basic structure:

```xml
<?xml version="1.0" encoding="utf-8"?>
<Report Width="10.5in" Name="YourReportName" xmlns="http://schemas.telerik.com/reporting/2025/2.0">
  <DataSources>
    <CsvDataSource RecordSeparators="&#xD;&#xA;" FieldSeparators="," HasHeaders="True" Name="csvDataSource1">
      <Source>
        <Uri Path="your_data.csv" />
      </Source>
      <Columns>
        <DataColumn Name="Column1" />
        <DataColumn Name="Column2" />
      </Columns>
    </CsvDataSource>
  </DataSources>
  <Items>
    <PageHeaderSection Height="1in" Name="pageHeaderSection1">
      <!-- Header content -->
    </PageHeaderSection>
    <DetailSection Height="0.5in" Name="detailSection1">
      <!-- Report body/table -->
    </DetailSection>
    <ReportFooterSection Height="1in" Name="reportFooterSection1">
      <!-- Summary/totals -->
    </ReportFooterSection>
    <PageFooterSection Height="0.5in" Name="pageFooterSection1">
      <!-- Page numbers -->
    </PageFooterSection>
  </Items>
  <PageSettings PaperKind="Letter" Landscape="True">
    <Margins>
      <MarginsU Left="0.5in" Right="0.5in" Top="0.5in" Bottom="0.5in" />
    </Margins>
  </PageSettings>
  <StyleSheet>
    <!-- Style rules -->
  </StyleSheet>
</Report>
```

## Common Expressions

### Formatting
- Currency: `Format("{0:C}", CDbl(Fields.Price))`
- Number with commas: `Format("{0:N0}", CDbl(Fields.Quantity))`
- Date: `Now().ToString("MM/dd/yyyy")`
- Percentage: `Format("{0:P}", CDbl(Fields.Rate))`

### Aggregates
- Count: `Count(Fields.FieldName)`
- Sum: `Sum(CDbl(Fields.FieldName))`
- Average: `Avg(CDbl(Fields.FieldName))`
- Min/Max: `Min(Fields.FieldName)` / `Max(Fields.FieldName)`

**Important: Using Aggregates in ReportFooterSection**

⚠️ **Known Issue**: Aggregate functions (Count, Sum, Avg, etc.) do **not reliably work** in ReportFooterSection when placed outside of Table components, even with `DataSourceName` attribute specified.

Tested approaches that **did NOT work**:
- Adding scope parameter: `Count(Fields.ID, 'table1')`
- Adding `DataSourceName` to individual TextBox elements
- Adding `DataSourceName` to ReportFooterSection container
- Adding `DataSourceName` to Report root element

**Recommended Workarounds**:

1. **Use Table Footer Rows** - Place aggregates in table structures where they work reliably:
```xml
<Table>
  <Body>
    <!-- Table cells with data -->
  </Body>
  <TableGroups>
    <TableGroup Name="grandTotal">
      <Groupings>
        <Grouping />
      </Groupings>
      <TableRows>
        <TableRow>
          <Cells>
            <TableCell>
              <TextBox Value="= 'Total: ' + Count(Fields.ID)" />
            </TableCell>
          </Cells>
        </TableRow>
      </TableRows>
    </TableGroup>
  </TableGroups>
</Table>
```

2. **Use Static Values** - For summary sections in ReportFooterSection, calculate totals manually and use static text:
```xml
<TextBox Value="Total Records: 20" />
<TextBox Value="Average Cost: $2,850" />
```

3. **Use Panel-Based Summaries** - Create formatted summary boxes with pre-calculated statistics for professional-looking reports without functional aggregates

### Calculations
- Addition: `CDbl(Fields.Field1) + CDbl(Fields.Field2)`
- Subtraction: `CDbl(Fields.Field1) - CDbl(Fields.Field2)`
- String concatenation: `Fields.FirstName + ' ' + Fields.LastName`

### Page Numbers
- Current page: `PageNumber`
- Total pages: `PageCount`
- Combined: `'Page ' + PageNumber + ' of ' + PageCount`

## BlueOpal Style Classes

The reports use the BlueOpal theme with these style names:
- `BlueOpal.TableNormal` - Table container
- `BlueOpal.TableHeader` - Table header cells (light blue background)
- `BlueOpal.TableBody` - Table data cells

## Vincue Color Palette

Use these colors consistently across all reports:

**Primary Colors:**
- **Dark Blue (Headings/Text)**: `0, 63, 89`
- **Steel Blue (Chart Bars)**: `70, 130, 180`
- **Light Blue Background**: `233, 244, 249`
- **Light Blue Borders**: `187, 220, 235`

**Secondary Colors:**
- **Forest Green (Positive/Actual)**: `34, 139, 34`
- **Red (Negative/Alerts)**: `Red` or `220, 53, 69`
- **Grey (Labels)**: `100, 100, 100`
- **Light Grey (Backgrounds)**: `250, 250, 250`
- **Light Grey (Borders)**: `220, 220, 220`
- **Grid Lines**: `200, 200, 200`

**Usage Guidelines:**
- Use Dark Blue `0, 63, 89` for all section headings and body text
- Use Steel Blue `70, 130, 180` for primary chart bars/data series
- Use Forest Green `34, 139, 34` for positive values, actual costs, or success indicators
- Use Red for negative variances, alerts, or over-budget items
- Use Grey `100, 100, 100` for secondary labels and subtitles

## Creating Charts and Graphs

Charts in Telerik Reporting require specific XML syntax. Follow these guidelines to avoid common errors:

### Basic Graph Structure

```xml
<Graph DataSourceName="csvDataSource1" Width="5in" Height="2.1in" Name="myChart">
  <PlotAreaStyle>
    <BackgroundColor>White</BackgroundColor>
  </PlotAreaStyle>

  <!-- Define Axes -->
  <Axes>
    <GraphAxis Name="categoryAxis">
      <MajorGridLineStyle Visible="False" />
      <Style>
        <Font Name="Segoe UI" Size="8pt" />
      </Style>
      <Scale>
        <CategoryScale />
      </Scale>
    </GraphAxis>
    <GraphAxis Name="valueAxis">
      <MajorGridLineStyle LineColor="200, 200, 200" />
      <Style>
        <Font Name="Segoe UI" Size="8pt" />
      </Style>
      <Scale>
        <NumericalScale />
      </Scale>
    </GraphAxis>
  </Axes>

  <!-- Link Axes to Coordinate System -->
  <CoordinateSystems>
    <CartesianCoordinateSystem XAxis="categoryAxis" YAxis="valueAxis" Name="cartesianCoordinateSystem1" />
  </CoordinateSystems>

  <!-- Define Series Groups (Required) -->
  <SeriesGroups>
    <GraphGroup Name="seriesGroup">
      <Groupings>
        <Grouping />
      </Groupings>
    </GraphGroup>
  </SeriesGroups>

  <!-- Define Category Groups -->
  <CategoryGroups>
    <GraphGroup Name="myGroup">
      <Groupings>
        <Grouping>=Fields.Category</Grouping>
      </Groupings>
      <Sortings>
        <Sorting>=Fields.Category</Sorting>
      </Sortings>
    </GraphGroup>
  </CategoryGroups>

  <!-- Define Series -->
  <Series>
    <BarSeries CoordinateSystem="cartesianCoordinateSystem1"
               CategoryGroup="myGroup"
               SeriesGroup="seriesGroup"
               Y="=Sum(CDbl(Fields.Value))"
               ArrangeMode="Clustered"
               Name="series1">
      <LegendItem>
        <Value>Series Name</Value>
      </LegendItem>
      <DataPointStyle LineWidth="0cm" />
      <DataPointConditionalFormatting>
        <FormattingRule>
          <Filters>
            <Filter Expression="= True" Operator="Equal" Value="= True" />
          </Filters>
          <Style>
            <BackgroundColor>70, 130, 180</BackgroundColor>
          </Style>
        </FormattingRule>
      </DataPointConditionalFormatting>
      <DataPointLabelStyle Visible="False" />
    </BarSeries>
  </Series>

  <Legend>
    <Style>
      <Font Name="Segoe UI" Size="9pt" />
    </Style>
  </Legend>

  <Style>
    <BorderStyle Default="Solid" />
    <BorderWidth Default="1pt" />
    <BorderColor Default="187, 220, 235" />
  </Style>
</Graph>
```

### Common Chart Errors and Fixes

**Error 1: "ReadElementContentAs() methods cannot be called on an element that has child elements"**
- **Cause**: Using `<CategoryAxis />` or `<NumericalAxis />` directly
- **Fix**: Use `<GraphAxis>` with `<Scale>` child element containing `<CategoryScale />` or `<NumericalScale />`

**Error 2: "LineStyle is not a valid value for LineStyle"**
- **Cause**: Using `<LineStyle>` or `<FillStyle>` as child elements in `DataPointStyle`
- **Fix**: Use `<DataPointStyle LineWidth="0cm" />` with no child elements, and set colors via `DataPointConditionalFormatting`

**Error 3: "LegendItem cannot be cast from String"**
- **Cause**: Using `LegendItem="Text"` as an attribute
- **Fix**: Use `<LegendItem><Value>Text</Value></LegendItem>` as a child element

**Error 4: "CategoryGroup cannot be null" or "SeriesGroup cannot be null"**
- **Cause**: Missing `CategoryGroup` or `SeriesGroup` attribute on BarSeries
- **Fix**: Add both attributes and define the groups at Graph level:
  - Add `SeriesGroups` and `CategoryGroups` collections at the Graph level (after CoordinateSystems)
  - Add `CategoryGroup="groupName"` and `SeriesGroup="seriesGroupName"` attributes to each BarSeries
  - Both attributes are **required** even if you don't need series grouping

**Error 5: Chart shows no data**
- **Cause**: Missing `DataSourceName` attribute on Graph element
- **Fix**: Add `DataSourceName="csvDataSource1"` to the Graph element

**Error 6: Data point label font size not applying**
- **Cause**: Using incorrect nested `<Style>` wrapper inside `DataPointLabelStyle`
- **Wrong Syntax**:
  ```xml
  <DataPointLabelStyle Visible="True">
    <Style>
      <Font Size="6pt" />  <!-- Extra <Style> wrapper prevents sizing -->
    </Style>
  </DataPointLabelStyle>
  ```
- **Correct Syntax**:
  ```xml
  <DataPointLabelStyle Visible="True">
    <Font Size="6pt" />  <!-- Font directly inside DataPointLabelStyle -->
  </DataPointLabelStyle>
  ```
- The `<Font>` element must be a direct child of `DataPointLabelStyle`, not wrapped in `<Style>`

### Chart Color Best Practices

- **Always use `DataPointConditionalFormatting`** to set bar/data point colors, not inline style attributes
- **Set colors with RGB values** like `<BackgroundColor>70, 130, 180</BackgroundColor>`
- **Use consistent Vincue colors** - Steel Blue for primary data, Forest Green for secondary
- **Match legend marker colors** using `LegendItem.MarkConditionalFormatting` if needed

### Key Requirements for Charts

1. **DataSourceName**: Must be set on the Graph element
2. **GraphAxis Names**: Must be unique and referenced in CartesianCoordinateSystem
3. **CategoryGroup Attribute**: Must match a GraphGroup Name
4. **CoordinateSystem**: BarSeries must reference the CartesianCoordinateSystem name
5. **Scale Elements**: Every GraphAxis needs a Scale child with CategoryScale or NumericalScale

## Page Footer Layout (Logo and Generated Date)

**IMPORTANT**: The Vincue logo and "Generated" date should **always be placed in the PageFooterSection**, not the header.

Standard footer layout uses a three-column design:
- **Left**: Generated date
- **Center**: Page numbers
- **Right**: Vincue logo

```xml
<PageFooterSection Height="0.5in" Name="pageFooterSection1">
  <Items>
    <!-- Left: Generated date -->
    <TextBox Width="3.5in" Height="0.3in" Left="0in" Top="0.1in"
             Value="= 'Generated: ' + Now().ToString(&quot;MM/dd/yyyy&quot;)"
             Name="dateTextBox">
      <Style>
        <Font Name="Segoe UI" Size="9pt" />
        <TextAlign>Left</TextAlign>
      </Style>
    </TextBox>

    <!-- Center: Page numbers -->
    <TextBox Width="4in" Height="0.3in" Left="3.5in" Top="0.1in"
             Value="= 'Page ' + PageNumber + ' of ' + PageCount"
             Name="pageNumberTextBox">
      <Style>
        <Font Name="Segoe UI" Size="9pt" />
        <TextAlign>Center</TextAlign>
      </Style>
    </TextBox>

    <!-- Right: Vincue logo -->
    <PictureBox Value="https://pro.vincue.com/images/VincueLogo_Black.png"
                Width="1in"
                Height="0.365in"
                Left="9in"
                Top="0.067in"
                Sizing="ScaleProportional"
                MimeType=""
                Name="vincueLogo" />
  </Items>
</PageFooterSection>
```

**Key Points:**
- Logo URL: `https://pro.vincue.com/images/VincueLogo_Black.png`
- `Sizing="ScaleProportional"` maintains aspect ratio
- Position logo at `Left="9in"` for right alignment on landscape reports (9in + 1in width = 10in, fits perfectly in usable area)
- Generated date uses `Now().ToString("MM/dd/yyyy")` expression
- All three elements should be in `PageFooterSection`, not `PageHeaderSection`

## PDF Export: Sizing and Layout Best Practices

### Understanding Usable Page Area

When designing reports for PDF export, you must account for the **usable page area**, which is the space available after margins are subtracted from the paper size.

**Formula for Letter Landscape (most common):**
- Paper size: 11in × 8.5in
- Margins: 0.5in on all sides (typical)
- **Usable Width** = 11in - 0.5in (left) - 0.5in (right) = **10in**
- **Usable Height** = 8.5in - 0.5in (top) - 0.5in (bottom) = **7.5in**

**For Detail/Report Footer sections:**
- Available height per page = 7.5in - PageHeader height - PageFooter height
- Example: 7.5in - 1.5in (header) - 0.5in (footer) = **5.5in per page**

### Critical Sizing Rules

1. **All report items must fit within usable width** - Never exceed 10in for Letter landscape with 0.5in margins
2. **Account for Left position** - If an item has `Left="0.25in"`, its width must be ≤ 9.75in
3. **Header TextBoxes should be 10in wide** (not 11in) to fit in usable area
4. **Tables must fit** - Ensure `Left + Width ≤ 10in` for the table
5. **Page Footer elements must fit** - Logo at `Left="9in"` with `Width="1in"` = 10in total

### Common PDF Export Issues and Fixes

**Issue**: "Pages are off" or content appears cut off in PDF
- **Cause**: Components exceed usable page width (10in for Letter landscape)
- **Fix**: Resize all components to fit within usable width
  - Headers/Titles: Change from 11in to 10in
  - Tables: Adjust width and position (e.g., 9.5in at Left="0.25in" = 9.75in total)
  - Footer logo: Position at Left="9in" (not 9.75in)

**Issue**: Content appears on wrong pages or splits unexpectedly
- **Cause**: ReportFooterSection height exceeds available space per page
- **Fix**: This is normal behavior - sections can span multiple pages. Design with this in mind.

**Issue**: Header label separated from its chart on different pages
- **Cause**: Header and chart are positioned separately, allowing page breaks between them
- **Fix**: Wrap related items in a Panel with `KeepTogether="True"`:
  ```xml
  <Panel Width="5in" Height="2.5in" Left="5.75in" Top="0.7in" Name="chartPanel">
    <Items>
      <TextBox Width="5in" Height="0.3in" Left="0in" Top="0in" Value="CHART TITLE" />
      <Graph Width="5in" Height="2.1in" Left="0in" Top="0.4in" ... />
    </Items>
    <KeepTogether>True</KeepTogether>
  </Panel>
  ```
  This prevents page breaks from splitting the header and chart.

**Issue**: Horizontal overflow creating blank pages
- **Cause**: Items positioned beyond usable width force horizontal pagination
- **Fix**: Use the formula: `Left position + Width ≤ 10in` for all items

**Issue**: Report spans too many pages with excessive white space
- **Cause**: Elements in ReportFooterSection positioned with too much vertical spacing
- **Fix**: Tighten up vertical positioning and reduce section height:
  - Reduce gaps between elements (e.g., change Top from 0.7in to 0.55in)
  - Reduce TextBox heights where content allows (e.g., 1.8in to 1.5in)
  - Move sections closer together (e.g., PERFORMANCE METRICS from 3.2in to 2.55in)
  - Reduce overall ReportFooterSection height (e.g., from 6in to 5in)
  - Align left and right column elements to end at similar vertical positions to optimize page breaks

### Design-Time Validation Checklist

Before exporting to PDF, verify:
- [ ] All TextBoxes: `Left + Width ≤ 10in`
- [ ] Tables: `Left + Width ≤ 10in`
- [ ] Page footer elements: Rightmost element ends at ≤ 10in
- [ ] Chart widths: Fit within 10in usable area
- [ ] Panel positions: Check all nested items fit
- [ ] Run `xmllint --noout yourfile.trdx` to validate XML syntax

### Iterative PDF Testing Process

Telerik recommends an iterative approach:
1. Design report in Report Designer
2. Export to PDF
3. Review the PDF output
4. Adjust sizing/positioning in Report Designer
5. Repeat until output is correct

Use this formula repeatedly: **Usable Width = Paper Width - Left Margin - Right Margin**

### Optimizing Page Count

To minimize unnecessary pages and white space:
1. **Tighten vertical spacing** - Reduce gaps between elements (use 0.05-0.15in gaps instead of 0.3-0.4in)
2. **Reduce TextBox heights** - Size boxes to fit content, not larger
3. **Compact ReportFooterSection** - Aim for the smallest height that accommodates your content
4. **Align column heights** - When using side-by-side columns, try to make them end at similar vertical positions
5. **Use KeepTogether sparingly** - Only use on critical grouped content, as it can force extra page breaks

### Balancing White Space

After optimizing for page count, you may have excessive white space (especially at page bottoms). To achieve better visual balance:

**Strategy: Expand components proportionally**
1. **Increase section height** - Add height to ReportFooterSection (e.g., 4.3in → 5in)
2. **Expand all text boxes** - Increase heights proportionally (e.g., 1.2in → 1.5in)
3. **Enlarge charts/graphs** - Make visualizations bigger for better readability (e.g., 1.75in → 2.15in)
4. **Increase font sizes** - Bump up headers and key metrics (e.g., 10pt → 11pt, 18pt → 22pt)
5. **Add spacing between sections** - Increase Top positions to spread content vertically

**Example from ReconditioningCostReport.trdx:**
- ReportFooterSection expanded from 4.3in to 5in
- KEY INSIGHTS text: 1.2in → 1.5in height
- Chart: 1.75in → 2.15in height
- Performance Metrics panel: 1.2in → 1.6in height
- Metric values: 18pt → 22pt font size
- All headers: 10pt → 11pt font size

**Result**: Better visual balance with content distributed across available space while maintaining target page count.

**Key Principle**: Once you've achieved the desired page count, incrementally expand components until white space is minimized but content doesn't overflow to extra pages. Test after each adjustment.

## Tips for Creating Reports

1. **Always specify the correct schema version** - Use the version that matches your Report Designer installation
2. **Define DataColumns explicitly** - List all CSV columns in the DataSource section
3. **Use landscape for wide tables** - Set `Landscape="True"` in PageSettings
4. **Format currency and numbers** - Use Format() expressions for proper display
5. **Include standard footer layout** - Add PageFooterSection with generated date (left), page numbers (center), and Vincue logo (right) - see "Page Footer Layout" section
6. **Use consistent colors** - Follow the Vincue Color Palette section for all styling
7. **Test with real data** - Ensure CSV file paths are correct and accessible
8. **Reference example reports** - Look at Report3FromDesigner.trdx and ReconditioningCostReport.trdx for structure
9. **Logo and date go in footer, not header** - Always place the Vincue logo and "Generated" date in PageFooterSection for consistent branding
10. **For charts, use proper Graph XML syntax** - See "Creating Charts and Graphs" section for complete examples and common error fixes
11. **Add DataSourceName to footer aggregates** - When using aggregate functions in ReportFooterSection, always add `DataSourceName="yourDataSourceName"` attribute to the TextBox (note: this may not work reliably - see Aggregates section above)
12. **Always design for usable page area, not paper size** - See "PDF Export: Sizing and Layout Best Practices" section above

## Troubleshooting Common Issues

### File Corruption After Git Operations

**Symptom**: Report file that was working fine starts crashing or throwing errors after git operations (checkout, revert, etc.), even though `git diff` shows no changes.

**Cause**: Potential file corruption during git operations, possibly related to line endings or binary encoding issues.

**Solution**:
```bash
# Extract a fresh copy from git
git show HEAD:ReconditioningCostReport.trdx > ReconditioningCostReport_fresh.trdx

# Test the fresh copy first
# If it works, replace the corrupted file
mv ReconditioningCostReport.trdx ReconditioningCostReport_corrupted.trdx
mv ReconditioningCostReport_fresh.trdx ReconditioningCostReport.trdx
```

**Prevention**: Always test reports in Telerik after any git operations before making further changes.

### Chart NullReferenceException Errors

**Symptom**: Report throws `System.NullReferenceException: Object reference not set to an instance of an object` when opening.

**Common Causes**:

1. **Malformed Grouping Expression**
   ```xml
   <!-- WRONG - This causes NullReferenceException -->
   <Grouping>="Total"</Grouping>

   <!-- CORRECT - Empty grouping -->
   <Grouping />

   <!-- CORRECT - Field-based grouping -->
   <Grouping Expression="= Fields.Year" />
   ```

2. **Missing or Incorrect Category Grouping**
   ```xml
   <!-- For charts grouped by a field, use this pattern: -->
   <CategoryGroups>
     <GraphGroup Name="categoryGroup1">
       <Groupings>
         <Grouping Expression="= Fields.Year" />
       </Groupings>
       <Sortings>
         <Sorting Expression="= Fields.Year" Direction="Asc" />
       </Sortings>
     </GraphGroup>
   </CategoryGroups>
   ```

3. **Data Type Mismatches** - Always use `CDbl()` to convert string fields to numbers:
   ```xml
   X="= Sum(CDbl(Fields.TotalActual))"
   ```

### Working with .trdp Files

**.trdp files are ZIP archives** containing report definitions and resources. To inspect or extract:

```bash
# Extract contents
unzip -o BarSeriesDataPointLabelAlignment.trdp -d extracted_folder

# View structure
unzip -l BarSeriesDataPointLabelAlignment.trdp
```

Contents typically include:
- `definition.xml` - The actual report definition (equivalent to .trdx contents)
- Data source files (CSV, etc.)
- `[Content_Types].xml` - Package manifest

This is useful when you need to reference working chart examples from .trdp files.

### Charts Grouped by Field Values

To create bar charts that group data by a field value (like Year) with visible labels:

```xml
<Graph DataSourceName="csvDataSource1" Width="5in" Height="2.5in" Name="chartByYear">
  <PlotAreaStyle LineWidth="0in" LineColor="LightGray" />
  <Axes>
    <GraphAxis Name="graphAxis1">
      <Scale><NumericalScale Minimum="0" /></Scale>
    </GraphAxis>
    <GraphAxis Name="graphAxis2">
      <Scale><CategoryScale /></Scale>
    </GraphAxis>
  </Axes>
  <CoordinateSystems>
    <CartesianCoordinateSystem XAxis="graphAxis1" YAxis="graphAxis2" />
  </CoordinateSystems>

  <!-- Define Series Group (required) -->
  <SeriesGroups>
    <GraphGroup Name="seriesGroup1" />
  </SeriesGroups>

  <!-- Group by the field value -->
  <CategoryGroups>
    <GraphGroup Name="categoryGroup1">
      <Groupings>
        <Grouping Expression="= Fields.Year" />
      </Groupings>
      <Sortings>
        <Sorting Expression="= Fields.Year" Direction="Asc" />
      </Sortings>
    </GraphGroup>
  </CategoryGroups>

  <!-- Multiple series for side-by-side bars -->
  <Series>
    <BarSeries CategoryGroup="categoryGroup1" SeriesGroup="seriesGroup1"
               X="= Sum(CDbl(Fields.TotalActual))"
               DataPointLabel="= Format(&quot;{0:C0}&quot;, Sum(CDbl(Fields.TotalActual)))">
      <DataPointStyle Visible="True" LineWidth="0in" />
      <DataPointLabelStyle Visible="True" />
      <LegendItem Value="'Total Actual'" />
    </BarSeries>
    <BarSeries CategoryGroup="categoryGroup1" SeriesGroup="seriesGroup1"
               X="= Sum(CDbl(Fields.TotalEstimate))"
               DataPointLabel="= Format(&quot;{0:C0}&quot;, Sum(CDbl(Fields.TotalEstimate)))">
      <DataPointStyle Visible="True" LineWidth="0in" />
      <DataPointLabelStyle Visible="True" />
      <LegendItem Value="'Total Estimate'" />
    </BarSeries>
  </Series>

  <Titles>
    <GraphTitle Text="Costs by Vehicle Year" Position="TopCenter">
      <Style LineWidth="0in" LineColor="LightGray" />
    </GraphTitle>
  </Titles>
</Graph>
```

**Key Points**:
- Use `Expression="= Fields.FieldName"` in the Grouping element
- Add Sortings to control the order of categories
- Multiple BarSeries with the same CategoryGroup creates side-by-side bars
- Set `DataPointLabelStyle Visible="True"` to show values on bars
- Format labels with `Format("{0:C0}", ...)` for currency without decimals

### Best Practices for Making Changes

1. **Make incremental changes** - Change one thing at a time
2. **Test after each change** - Open the report in Telerik Designer after every edit
3. **Validate XML syntax** - Use `xmllint --noout file.trdx` to check for XML errors
4. **Keep backups** - Save working versions before making complex changes
5. **Use git commits** - Commit working versions frequently
6. **Start simple** - Begin with minimal examples and add complexity gradually

## Resources

- [Telerik Reporting Documentation](https://docs.telerik.com/reporting/)
- [XML Report Definition Guide](https://docs.telerik.com/reporting/designing-reports/report-designer-tools/desktop-designers/standalone-report-designer/xml-report-definition)
