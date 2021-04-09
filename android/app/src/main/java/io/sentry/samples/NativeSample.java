package io.sentry.samples;

public class NativeSample {
    public static native void message();

    public static native void crash();

    static {
        System.loadLibrary("native-sample");
    }
}