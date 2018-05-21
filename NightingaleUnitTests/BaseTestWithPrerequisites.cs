using System;
using System.IO;
using System.Linq;
using System.Text;
using Nightingale;
using NUnit.Framework;
using System.Threading;
using System.Data.SQLite;
using System.Collections.Generic;

namespace NightingaleUnitTests
{
    public class BaseTestWithPrerequisites
    {
        // This is a pattern I haven't really explored, but.
        // This is a global variable indicating whether or not the test being run is the
        // prerequisite for another test.
        // If the test fails, it should give the default error message, but if a prerequisite
        // test fails, the error message should be to fix that prerequisite test first.
        protected string _runningPrerequisiteErrorMessage;


        protected delegate void PrerequisiteTest();

        protected void TestPrerequisite(PrerequisiteTest test)
        {
            _runningPrerequisiteErrorMessage = "Prerequisite '" + test.Method.Name + "' not met";
            test.Invoke();
            _runningPrerequisiteErrorMessage = null;
        }
    }
}
