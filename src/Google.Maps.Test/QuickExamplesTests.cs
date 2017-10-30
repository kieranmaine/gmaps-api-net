﻿using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Google.Maps.Direction;
using Google.Maps.Geocoding;
using Google.Maps.StaticMaps;

namespace Google.Maps
{
	/*
	 *  This test fixture was set up to ensure that the online samples work as written and as expected.
	 */
	[TestFixture]
	class QuickExamplesTests
	{

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			this.TestingApiKey = SigningHelper.GetApiKey();
			this.GMaps = new Services(TestingApiKey);
		}
		GoogleSigned TestingApiKey;
		Google.Maps.Services GMaps;

		[Test]
		public void GeocodingRequest_Example()
		{
			//var GMaps = new Google.Maps.Services("YOUR_API_KEY");
			
			var request = new GeocodingRequest();
			request.Address = "1600 Amphitheatre Parkway";
			var response = GMaps.GeocodingService.GetResponse(request);

			// --break in the online version here-- //

			var result = response.Results.First();

			Console.WriteLine("Full Address: " + result.FormattedAddress);         // "1600 Amphitheatre Pkwy, Mountain View, CA 94043, USA"
			Console.WriteLine("Latitude: " + result.Geometry.Location.Latitude);   // 37.4230180
			Console.WriteLine("Longitude: " + result.Geometry.Location.Longitude); // -122.0818530

			Assert.Pass();
		}

		[Test]
		public void StaticMapRequest_Example()
		{
			var map = new StaticMapRequest();
			map.Center = new Location("1600 Amphitheatre Parkway Mountain View, CA 94043");
			map.Size = new MapSize(400, 400);
			map.Zoom = 14;

			var imgTagSrc = map.ToUri();

			Assert.Pass();
		}

		[Test]
		public void PartialMatchTest()
		{
			// invalid address results in partial match
			var request = new DirectionRequest
			{
				Origin = new Location("410 Beeeeeechwood Rd, NJ 07450"),
				Destination = new Location("204 Powell Ave, CA 94523")
			};
			var response = GMaps.DirectionService.GetResponse(request);

			Assert.True(response.Waypoints.Any(wp => wp.PartialMatch));
		}
	}
}
