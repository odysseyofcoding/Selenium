using System;
using System.Collections.Generic;
using System.Drawing;
using Emgu.CV;
using Emgu.CV.Structure;

namespace YourProgram
{
    class FindWebElementWithTemplateMatching
    {
        public List<Tuple<string, Rectangle>> CropAndCompareTemplate(Bitmap screenshot)
        {
            //Contains templates you want to search for
            List<Bitmap> myTemplates = new List<Bitmap>
            {
                (Bitmap)Image.FromFile(@""),
                (Bitmap)Image.FromFile(@""),
                (Bitmap)Image.FromFile(@"") 
            };
            
            //To return the List with the description and Rectangle of the webelement
            List<Tuple<string, Rectangle>> myTupleOfRects = new List<Tuple<string, Rectangle>>();
            
            foreach (var item in myTemplates)
            {
                try
                {
                    var original = screenshot
                        .ToImage<Bgr, byte>();
                    var template = item.ToImage<Bgr, byte>();
                    
                    // Match template first time
                    Mat imgOut = new Mat();
                    CvInvoke.MatchTemplate(original, template, imgOut, Emgu.CV.CvEnum.TemplateMatchingType.Sqdiff);

                    Mat imgOutNorm = new Mat();
                    CvInvoke.Normalize(imgOut, imgOutNorm, 0, 1, Emgu.CV.CvEnum.NormType.MinMax);

                    Matrix<double> matches = new Matrix<double>(imgOutNorm.Size);
                    imgOutNorm.CopyTo(matches);

                    double minValue = 0, maxValue = 0;
                    Point minLoc = new Point();
                    Point maxLoc = new Point();
                    
                    // Get the Locations for drawing on the original image
                    CvInvoke.MinMaxLoc(matches, ref minValue, ref maxValue, ref minLoc, ref maxLoc);
                    Rectangle r = new Rectangle(minLoc, template.Size);
                    
                    //store a copy of original into a temporary variable to compare it with the template
                    var temporaryImage = original.Copy();
                    
                    //crops the image at the detected location before counter comparing because if the template will not be displayed the first matching will return a false rectangle
                    temporaryImage.ROI = r;
                    CvInvoke.cvResetImageROI(temporaryImage);
                    
                    //compares the image for differences
                    Image<Gray, float> resultImage = temporaryImage.MatchTemplate(template, Emgu.CV.CvEnum.TemplateMatchingType.CcoeffNormed);
                    
                    // Get the locations of the result image to check the similarity
                    resultImage.MinMax(out double[] minValues, out double[] maxValues, out Point[] minLocations, out Point[] maxLocations);
                    
                    // tell the score, more to 1 means it is matching
                    if (maxValues[0] > 0.999)
                    {
                        //Console.WriteLine(bitmaps.IndexOf(item));
                        if (myTemplates.IndexOf(item) == 0)
                        {
                            myTupleOfRects.Add(Tuple.Create("name of webelement", r));
                        }
                        else if (myTemplates.IndexOf(item) == 1)
                        {
                            myTupleOfRects.Add(Tuple.Create("name of webelement", r));
                        }
                        else if (myTemplates.IndexOf(item) == 2)
                        {
                            myTupleOfRects.Add(Tuple.Create("name of webelement", r));                        
                        }
                    }
                    else
                    {
                        //Console.WriteLine($"not really same{bitmaps.IndexOf(item)}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return myTupleOfRects;
        }
    }
}
