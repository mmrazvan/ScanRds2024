﻿namespace ScanApp.Helpers;

public static class ProgressHelper
{
	public static void UpdateProgressbar( ProgressBar p, int value, int max )
	{
		p.Maximum = max;
		p.Value = value;
	}

	public static void UpdateProgressbar( ProgressBar p, int value, int max, Label l )
	{
		p.Maximum = max;
		p.Value = value;
		l.Text = $"{value} / {max} ({CalculatePercentage(value, max)})";
	}

	private static string CalculatePercentage( int value, int max )
	{
		return $"{( double ) value / max * 100:0.00}%";
	}
}