/* ======================================================= //
// ===--- SCRIPT: MotorovaRotors ----------------------=== //
// ===--- AUTHOR: Qamuy -------------------------------=== //
// ===--- FOR: Pilarka --------------------------------=== //
// ======================================================= */

const string CAPTAIN_COCKPIT_TAG = "[QP-CC]";
const string ROTOR_PUSH_TAG = "[QP-RW]";
const string ROTOR_BREAK_TAG = "[QP-RS]";

const string d = "Displacement";
const float p = 20 f;
const float b = -40 f;

Vector3D n, t;

public Program() {
  Runtime.UpdateFrequency = UpdateFrequency.Update1;
}

public void Main(string argument, UpdateType updateSource) {
  var c = new List < IMyTerminalBlock > ();
  GridTerminalSystem.SearchBlocksOfName(CAPTAIN_COCKPIT_TAG, c);
  var w = new List < IMyTerminalBlock > ();
  GridTerminalSystem.SearchBlocksOfName(ROTOR_PUSH_TAG, w);
  var s = new List < IMyTerminalBlock > ();
  GridTerminalSystem.SearchBlocksOfName(ROTOR_BREAK_TAG, s);
  Vector3D v = new Vector3D();
  foreach(IMyShipController rc in c) {
    if (rc.IsUnderControl) {
      v.X = rc.MoveIndicator.Z;
      v.Y = rc.MoveIndicator.X;
      v.Z = rc.MoveIndicator.Y;
    }
  }
  n = Me.GetPosition();
  if (Vector3D.Distance(n, t) > 0.0001) {
    if (v.X < 0) {
      foreach(IMyMotorStator r in w) {
        r.SetValue < float > (d, p);
      }
      foreach(IMyMotorStator r in s) {
        r.SetValue < float > (d, b);
      }
    } else if (v.X > 0) {
      foreach(IMyMotorStator r in w) {
        r.SetValue < float > (d, b);
      }
      foreach(IMyMotorStator r in s) {
        r.SetValue < float > (d, p);
      }
    } else {
      foreach(IMyMotorStator r in w) {
        r.SetValue < float > (d, b);
      }
      foreach(IMyMotorStator r in s) {
        r.SetValue < float > (d, b);
      }
    }
  }
  t = n;
}
