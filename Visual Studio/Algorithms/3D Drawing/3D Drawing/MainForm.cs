using System;
using System.Windows.Forms;

namespace ThreeDDrawing
{
    public partial class MainForm : Form
    {
        private Scene3DBase scene;

        private const double rotate_angle = Math.PI / 180.0;
        private const double angle_of_view_step = Math.PI / 180.0;

        public MainForm()
        {
            InitializeComponent();
            scene = new Scene3D(canvas3DMain.ClientSize);
        }

        private void canvas3DMain_Paint(object sender, PaintEventArgs e)
        {
            scene.Render(e.Graphics);
        }

        private void canvas3DMain_ClientSizeChanged(object sender, EventArgs e)
        {
            scene.Resize(canvas3DMain.ClientSize);
            canvas3DMain.Invalidate();
        }

        private void canvas3DMain_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.Shift)
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        scene.Camera.Transform.Pitch(-rotate_angle / 10.0);
                        break;

                    case Keys.Down:
                        scene.Camera.Transform.Pitch(rotate_angle / 10.0);
                        break;

                    case Keys.Right:
                        scene.Camera.Transform.Yaw(rotate_angle / 10.0);
                        break;

                    case Keys.Left:
                        scene.Camera.Transform.Yaw(-rotate_angle / 10.0);
                        break;

                    case Keys.Add:
                        scene.Camera.Transform.Roll(rotate_angle);
                        break;

                    case Keys.Subtract:
                        scene.Camera.Transform.Roll(-rotate_angle);
                        break;

                    default:
                        return;
                }
            }
            else
            {
                switch (e.KeyCode)
                {
                    case Keys.Up:
                        scene.Camera.Transform.MoveDown(-0.1);
                        break;

                    case Keys.Down:
                        scene.Camera.Transform.MoveDown(0.1);
                        break;

                    case Keys.Right:
                        scene.Camera.Transform.MoveRight(0.1);
                        break;

                    case Keys.Left:
                        scene.Camera.Transform.MoveRight(-0.1);
                        break;

                    case Keys.Add:
                        scene.Camera.Transform.MoveForward(0.1);
                        break;

                    case Keys.Subtract:
                        scene.Camera.Transform.MoveForward(-0.1);
                        break;

                    case Keys.A:
                        scene.Camera.AngleOfView += angle_of_view_step;
                        scene.Camera.CommitFVSAndAOV();
                        break;

                    case Keys.Z:
                        if (scene.Camera.AngleOfView - angle_of_view_step < angle_of_view_step)
                        {
                            scene.Camera.AngleOfView = angle_of_view_step;
                        }
                        else
                        {
                            scene.Camera.AngleOfView -= angle_of_view_step;
                        }
                        scene.Camera.CommitFVSAndAOV();
                        break;

                    case Keys.R:
                        scene.Camera.Reset();
                        break;

                    default:
                        return;
                }
            }
            canvas3DMain.Invalidate();
        }
    }
}
