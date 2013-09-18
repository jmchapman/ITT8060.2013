
#light

open System

let data = "Test data,1233"

let convertDataRow(csvLine:string) = 
  let cells = List.ofSeq(csvLine.Split(','))
  match cells with
  | title :: number :: _ ->
    let parsedNumber = Int32.Parse(number)
    (title, parsedNumber)
  | _ -> failwith "Incorrect data format!"

convertDataRow(data)

String.Concat("first part", "second part")

// F#!
String.concat ", " ["One"; "Two" ; "Three"]

String.Join(", ", [| "One"; "Two" ; "Three" |] )


let anArray = [| "One"; "Two" ; "Three" |]

let aList = ["One"; "Two" ; "Three"]

let rec processLines (lines) = 
  match lines with
  | [] -> []
  | currentLine :: remaining ->
    let parsedLine = convertDataRow(currentLine)
    let parsedRest = processLines(remaining)
    parsedLine :: parsedRest

open System.IO

let lines = List.ofSeq(File.ReadAllLines(@"c:\tmp\data.csv"))

processLines(lines)

let rec calculateSum (rows) = 
  match rows with
  | [] -> 0
  | (_, value) :: tail ->
    let remainingSum = calculateSum(tail)
    value + remainingSum

calculateSum  (processLines (lines))

let i1 = 1233

let i2 = 1233u
let i3 = 1233L
let i4 = 1233L

let f1 = 3.4
let f2 = 3.14f

// this is an F# float!
let f11 = Double.Parse("3,14")

let b1 = 127y
let b2 = 255uy

let d = 3.14M

let bigint = 1I

let (succ, num) = Int32.TryParse("123")
if succ then Console.Write("Succeeded: {0}", num)
else Console.Write("Failed!")

open System
open System.Drawing
open System.Windows.Forms
let mainForm = new Form(Width = 620, Height=450, Text = "Pie chart")

let menu = new ToolStrip()
let btnOpen = new ToolStripButton("Open")
let btnSave = new ToolStripButton("Save")
ignore (menu.Items.Add(btnOpen))
ignore (menu.Items.Add(btnSave))

let boxChart = 
  new PictureBox
    (BackColor = Color.White, Dock = DockStyle.Fill,
    SizeMode = PictureBoxSizeMode.CenterImage)

mainForm.Controls.Add(menu)
mainForm.Controls.Add(boxChart)

//[<STAThread>]
//do
  //Application.Run(mainForm)
  //mainForm.Show()

let rnd = new Random()

let randomBrush() = 
  let r, g, b = rnd.Next(256), rnd.Next(256), rnd.Next(256)
  new SolidBrush(Color.FromArgb(r,g,b))

let fnt = new Font("Times New Roman", 11.0f)

let centerX, centerY  = 300.0, 200.0
let labelDistance = 150.0

let drawLabel(gr:Graphics, title, startAngle, angle) = 
  let lblAngle = float (startAngle + angle / 2)
  let ra = Math.PI * 2.0 * lblAngle / 360.0
  let x = centerX + labelDistance * cos(ra)
  let y = centerY + labelDistance * sin(ra)
  let size = gr.MeasureString(title,fnt)
  let rc = new PointF(float32(x) - size.Width / 2.0f,
                      float32(y) - size.Height / 2.0f)
  gr.DrawString(title,fnt,Brushes.Black,new RectangleF(rc, size))



let drawPieSegment(gr:Graphics, title, startAngle,occupiedAngle) = 
  let br = randomBrush()
  gr.FillPie (br , 170, 70, 260, 260, startAngle, occupiedAngle)
  br.Dispose()

let drawStep (drawingFunc, gr:Graphics, sum, data) = 
  let rec drawStepUtil (data, angleSoFar) = 
    match data with
    | [] -> ()
    | [title, value] -> 
      let angle = 360 - angleSoFar
      drawingFunc(gr, title, angleSoFar, angle)
    | (title, value) :: tail -> 
      let angle = int (float(value) / sum * 360.0)
      drawingFunc(gr,title, angleSoFar,angle)
      drawStepUtil(tail,angleSoFar + angle)
  drawStepUtil(data,0)

let drawChart(file) =
  let lines = List.ofSeq (File.ReadAllLines(file))
  let data = processLines(lines)
  let sum = float (calculateSum(data))

  let pieChart = new Bitmap(600, 400)
  let gr = Graphics.FromImage(pieChart)
  gr.Clear(Color.White)
  drawStep(drawPieSegment, gr, sum, data)
  drawStep(drawLabel, gr, sum, data)

  gr.Dispose()
  pieChart

let openAndDrawChart(e) = 
  let dlg = new OpenFileDialog(Filter="CSV Files|*.csv")
  if (dlg.ShowDialog() = DialogResult.OK) then
    let pieChart = drawChart(dlg.FileName)
    boxChart.Image <- pieChart
    btnSave.Enabled <- true





[<STAThread>]
do
  btnOpen.Click.Add(openAndDrawChart)
  //Application.Run(mainForm)
  mainForm.Show()