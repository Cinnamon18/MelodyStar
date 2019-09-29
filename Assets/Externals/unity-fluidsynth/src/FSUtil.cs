using System;
using System.Collections;
using UnityEngine;

namespace FluidSynth {
    public static class Util {
        /*
         * Function signature for status callbacks.
         */
        public delegate void Callback();

        /*
         * Perform a logged error check of a boolean condition.
         * @param condition
         * @param format string for error message if condition is false
         * @param parameters for format string
         */
        public static bool Check
            (bool cond, String fmt="", params object[] args) {
            if(!cond && fmt != "")
                Debug.LogError(String.Format(fmt, args));
            return cond;
        }

        /*
         * Perform a logged error check of a boolean condition.
         * @param condition
         * @param callback function if condition is true
         * @param callback function if condition is false
         * @param format string for error message if condition is false
         * @param parameters for format string
         */
        public static bool Check(bool cond, Callback pass, Callback fail,
                   String fmt="", params object[] args) {
            if(cond)
                pass();
            else
                fail();
            return Check(cond, fmt, args);
        }

        /*
         * Perform an exception check of a boolean condition.
         * @param condition
         * @param format string for error message if condition is false
         * @param parameters for format string
         */
        public static void Assert
            (bool cond, String fmt="", params object[] args) {
            if(!cond && fmt != "")
                throw new Exception(String.Format(fmt, args));
        }

        /*
         * Perform an exception check of a boolean condition.
         * @param condition
         * @param callback function if condition is true
         * @param callback function if condition is false
         * @param format string for error message if condition is false
         * @param parameters for format string
         */
        public static void Assert(bool cond, Callback pass, Callback fail,
                    String fmt="", params object[] args) {
            if(cond)
                pass();
            else
                fail();
            Assert(cond, fmt, args);
        }
    }
}
