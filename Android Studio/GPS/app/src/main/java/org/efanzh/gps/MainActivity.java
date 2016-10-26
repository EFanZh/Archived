package org.efanzh.gps;

import android.app.Activity;
import android.content.Context;
import android.location.Criteria;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.widget.TextView;

import java.util.Date;

public class MainActivity extends Activity implements LocationListener
{
    TextView textView;

    @Override
    protected void onCreate(Bundle savedInstanceState)
    {
        super.onCreate(savedInstanceState);

        textView = new TextView(this);
        this.setContentView(textView);

        LocationManager locationManager = (LocationManager) this.getSystemService(Context.LOCATION_SERVICE);

        textView.setText("Requesting location updates…");

        try
        {
            Criteria criteria = new Criteria();

            criteria.setAccuracy(Criteria.ACCURACY_FINE);

            locationManager.requestLocationUpdates(0, 0, criteria, this, null);
        }
        catch (SecurityException e)
        {
            textView.setText("Unable to request location updates.");
        }
    }

    @Override
    public void onLocationChanged(Location location)
    {
        StringBuilder stringBuilder = new StringBuilder("Longitude: ");

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
        textView.setText("Status: " + status + '.');
    }

    @Override
    public void onProviderEnabled(String provider)
    {
        textView.setText("Provider " + provider + " is enabled.");
    }

    @Override
    public void onProviderDisabled(String provider)
    {
        textView.setText("Provider " + provider + " is disabled.");
    }
}
