using System;
using NXOpen;
using NXOpen.UF;
using System.Collections;
using System.Threading;
using System.Globalization;

namespace NX_Plugin
{
    public class Program
    {
        // class members
        private static UFSession theUfSession;
        public static Program theProgram;
        public static bool isDisposeCalled;

        //------------------------------------------------------------------------------
        // Constructor
        //------------------------------------------------------------------------------
        public Program()
        {
            try
            {
                //theSession = Session.GetSession();
                //theUI = UI.GetUI();
                theUfSession = UFSession.GetUFSession();
                isDisposeCalled = false;
            }
            catch (NXOpen.NXException)
            {
                // ---- Enter your exception handling code here -----
                // UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
            }
        }

        //------------------------------------------------------------------------------
        //  Explicit Activation
        //      This entry point is used to activate the application explicitly
        //------------------------------------------------------------------------------
        public static int Main(string[] args)
        {
            // System.Diagnostics.Debugger.Launch();

            int retValue = 0;
            try
            {
                theProgram = new Program();

                Form1 form = new Form1();

                form.Show();

                theProgram.Dispose();
            }
            catch (NXOpen.NXException ex)
            {
                // ---- Enter your exception handling code here -----
                UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
            }
            return retValue;
        }

        public static void CreateModel(double d, double l, double b, double h, double C)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            Tag UFPart1;
            theUfSession.Part.New("model_ka", 1, out UFPart1);

            Tag cylinder;
            double[] point = { 0, 0, 0 };
            double[] direction = { 1, 0, 0 };
            theUfSession.Modl.CreateCylinder(FeatureSigns.Nullsign, UFPart1, point, l.ToString(), d.ToString(), direction, out cylinder);


            Tag cyl_tag, obj_id_camf;
            Tag[] Edge_array_cyl;
            int ecount;
            theUfSession.Modl.AskFeatBody(cylinder, out cyl_tag);
            theUfSession.Modl.AskBodyEdges(cyl_tag, out Edge_array_cyl);
            theUfSession.Modl.AskListCount(Edge_array_cyl, out ecount);

            ArrayList arr_list = new ArrayList();

            for (int i = 0; i < ecount; ++i)
            {
                Tag edge;
                theUfSession.Modl.AskListItem(Edge_array_cyl, i, out edge);
                arr_list.Add(edge);
            }

            Tag[] list = (Tag[])arr_list.ToArray(typeof(Tag));
            theUfSession.Modl.CreateChamfer(3, C.ToString(), C.ToString(), "45", list, out obj_id_camf);

            ////////////////////////////////
            double r = d / 2;
            double w = h / 2;
            double[] l1_endpt1 = { 0, -r, -w };
            double[] l1_endpt2 = { 0, r, -w };
            double[] l2_endpt1 = l1_endpt2;
            double[] l2_endpt2 = { 0, r, w };
            double[] l3_endpt1 = l2_endpt2;
            double[] l3_endpt2 = { 0, -r, w };
            double[] l4_endpt1 = l3_endpt2;
            double[] l4_endpt2 = l1_endpt1;

            UFCurve.Line[] lines = {
                        new UFCurve.Line() { start_point = l1_endpt1, end_point = l1_endpt2 },
                        new UFCurve.Line() { start_point = l2_endpt1, end_point = l2_endpt2 },
                        new UFCurve.Line() { start_point = l3_endpt1, end_point = l3_endpt2 },
                        new UFCurve.Line() { start_point = l4_endpt1, end_point = l4_endpt2 }
                    };

            Tag[] lines_tags = new Tag[lines.Length];
            for (int i = 0; i < lines.Length; ++i)
                theUfSession.Curve.CreateLine(ref lines[i], out lines_tags[i]);

            string[] limit4 = { "0", h.ToString() };
            double[] dummy = new double[3];
            double[] direction4 = { 1.0, 0.0, 0.0 };
            Tag[] extruded;

            theUfSession.Modl.CreateExtruded(
                            lines_tags,
                            "0",
                            limit4,
                            dummy,
                            direction4,
                            FeatureSigns.Negative,
                            out extruded);
        }


        //------------------------------------------------------------------------------
        // Following method disposes all the class members
        //------------------------------------------------------------------------------
        public void Dispose()
        {
            try
            {
                if (isDisposeCalled == false)
                {
                    //TODO: Add your application code here 
                }
                isDisposeCalled = true;
            }
            catch (NXOpen.NXException)
            {
                // ---- Enter your exception handling code here -----

            }
        }
    }
}