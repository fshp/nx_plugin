using System;
using NXOpen;
using NXOpen.UF;
using System.Collections;

namespace NX_Plugin
{
    public class Program
    {
        // class members
        private static Session theSession;
        private static UI theUI;
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
                theSession = Session.GetSession();
                theUI = UI.GetUI();
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

                Tag UFPart1;
                string name1 = "model_k";
                int units1 = 1;
                theUfSession.Part.New(name1, units1, out UFPart1);

                const double l = 50;
                const double r = 5;

                Tag cylinder;
                double[] point = { 0, 0, 0 };
                double[] direction = { 1, 0, 0 };
                theUfSession.Modl.CreateCylinder(FeatureSigns.Nullsign, UFPart1, point, l.ToString(), (r*2).ToString(), direction, out cylinder);


                Tag cyl_tag, obj_id_camf;
                Tag[] Edge_array_cyl;
                int ecount;

                theUfSession.Modl.AskFeatBody(cylinder, out cyl_tag);
                theUfSession.Modl.AskBodyEdges(cyl_tag, out Edge_array_cyl);
                theUfSession.Modl.AskListCount(Edge_array_cyl, out ecount);

                ArrayList arr_list = new ArrayList();

                for (int ii = 0; ii < ecount; ii++)
                {
                    Tag edge;

                    theUfSession.Modl.AskListItem(Edge_array_cyl, ii, out edge);
                    arr_list.Add(edge);
                }
                
                Tag[] list = (Tag[])arr_list.ToArray(typeof(Tag));
                string offset1 = "1";
                string offset2 = "1";
                string ang = "45";
                theUfSession.Modl.CreateChamfer(3, offset1, offset2, ang, list, out obj_id_camf);


                ////////////////////////////////


                {
                    const double rp = r / 6;

                    double[] ll1_endpt1 = { 0, -r, -rp };
                    double[] ll1_endpt2 = { 0, r, -rp };
                    double[] ll2_endpt1 = ll1_endpt2;
                    double[] ll2_endpt2 = { 0, r, rp };
                    double[] ll3_endpt1 = ll2_endpt2;
                    double[] ll3_endpt2 = { 0, -r, rp };
                    double[] ll4_endpt1 = ll3_endpt2;
                    double[] ll4_endpt2 = ll1_endpt1;

                    UFCurve.Line[] llines = {
                        new UFCurve.Line() { start_point = ll1_endpt1, end_point = ll1_endpt2 },
                        new UFCurve.Line() { start_point = ll2_endpt1, end_point = ll2_endpt2 },
                        new UFCurve.Line() { start_point = ll3_endpt1, end_point = ll3_endpt2 },
                        new UFCurve.Line() { start_point = ll4_endpt1, end_point = ll4_endpt2 }
                    };

                    Tag[] llines_tags = new Tag[llines.Length];
                    for (int i = 0; i < llines.Length; ++i)
                        theUfSession.Curve.CreateLine(ref llines[i], out llines_tags[i]);

                    string taper_angle4 = "0.0";
                    double h = 3;
                    string[] limit4 = { "0", h.ToString() };
                    double[] ref_pt4 = new double[3];
                    double[] direction4 = { 1.0, 0.0, 0.0 };
                    Tag[] features4;

                    theUfSession.Modl.CreateExtruded(
                                    llines_tags,
                                    taper_angle4,
                                    limit4,
                                    ref_pt4,
                                    direction4, FeatureSigns.Negative,
                                    out features4);


                    

                } 

                theProgram.Dispose();


            }
            catch (NXOpen.NXException)
            {
                // ---- Enter your exception handling code here -----
                UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, "Error");

            }
            return retValue;
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