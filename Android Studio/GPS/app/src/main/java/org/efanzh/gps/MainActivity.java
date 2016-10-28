package org.efanzh.gps;

import android.app.*;
import android.content.*;
import android.location.*;
import android.os.*;
import android.widget.*;

import java.util.*;

public class MainActivity extends Activity implements LocationListener
{
    TextView textView;
    StringBuilder stringBuilder = new StringBuilder();

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);

        textView = new TextView(this);

        this.setContentView(textView);

        LocationManager locationManager = (LocationManager)this.getSystemService(Context.LOCATION_SERVICE);

        Criteria criteria = new Criteria();

        criteria.setAccuracy(Criteria.ACCURACY_FINE);

        try
        {
            locationManager.requestLocationUpdates(0, 0, criteria, this, null);

            textView.setText("Waiting for location updates…");
        }
        catch (SecurityException e)
        {
            textView.setText("Unable to request location updates.");
        }
    }

    @Override
    public void onLocationChanged(Location location)
    {
        stringBuilder.setLength(0);
        stringBuilder.append("Longitude: ");
        stringBuilder.append(location.getLongitude());

        stringBuilder.append("°\nLatitude: ");
        stringBuilder.append(location.getLatitude());
        stringBuilder.append('°');

        if (location.hasAltitude())
        {
            stringBuilder.append("\nAltitude: ");
            stringBuilder.append(location.getAltitude());
            stringBuilder.append(" m");
        }

        if (location.hasAccuracy())
        {
            stringBuilder.append("\nAccuracy: ");
            stringBuilder.append(location.getAccuracy());
            stringBuilder.append(" m");
        }

        if (location.hasBearing())
        {
            stringBuilder.append("\nBearing: ");
            stringBuilder.append(location.getBearing());
            stringBuilder.append('°');
        }

        if (location.hasSpeed())
        {
            stringBuilder.append("\nSpeed: ");
            stringBuilder.append(location.getSpeed());
            stringBuilder.append(" m/s");
        }

        stringBuilder.append("\nTime: ");
        stringBuilder.append(new Date(location.getTime()));

        stringBuilder.append("\nProvider: ");
        stringBuilder.append(location.getProvider());

        textView.setText(stringBuilder.toString());
    }

    @Override
    public void onStatusChanged(String provider, int status, Bundle extras)
    {
    }

    @Override
    public void onProviderEnabled(String provider)
    {
    }

    @Override
    public void onProviderDisabled(String provider)
    {
    }
}
