namespace Lecture8Library

(* Lecture 8, 2nd half: Using F# from other .NET languages *)

#if INTERACTIVE

#endif


open System.Drawing

type Rect =
  { Left : float32; Top : float32
    Height : float32; Width : float32 }

  // NB! indendation is important!
  member x.Deflate(vspace, hspace) =
    { Left = x.Left - hspace
      Top = x.Top - vspace
      Height = x.Height - (2.0f * vspace)
      Width = x.Width - (2.0f * hspace)
    }

  member x.Convert() =
      new RectangleF(x.Left,x.Top,x.Width,x.Height)

type Client =
  {Income: string; Name: string; CriminalRecord : bool}

type ClientTest =
  abstract Check : Client -> bool
  abstract Report : Client -> unit

// You can define functions corresponding to static functions in C# by
// defining a module.

module MyModule =

  let SomeVariable = 42

  let Add x y = 
    x+y
  
;;