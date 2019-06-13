using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web;
using WebApp.Models;
using WebApp.Persistence.UnitOfWork;

namespace WebApp.Hubs
{
	[HubName("lokacija")]
	public class LocationHub : Hub
	{
		private static IHubContext hubContext = GlobalHost.ConnectionManager.GetHubContext<LocationHub>();

		private static Timer timer = new Timer();

		IUnitOfWork UnitOfWork { get; set; }
		public LocationHub(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
	
		}

		public void Start()
		{
			if (!timer.Enabled)
			{
				timer.Interval = 5000;
				timer.Start();
				timer.Elapsed += OnTimedEvent;
			}
		}

		private async void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			await SendLocation();
		}

		public void Stop()
		{
			timer.Stop();
		}

		private async Task SendLocation()
		{
			var l = UnitOfWork.Linije.GetAll();
			List <Linija> linije = l.ToList();
			StringBuilder lokacije = new StringBuilder("");
			Random rnd = new Random();

			foreach (var item in linije)
			{
				int index = rnd.Next(0, item.Stanice.Count - 1);
				var stanica = item.Stanice.ToList().ElementAt(index);
				lokacije.Append($"{item.Ime}_{stanica.X}_{stanica.Y};");
			}

			await Clients.Group("Admins").getLocation(lokacije.ToString());

		}

		public override Task OnConnected()
		{
			Groups.Add(Context.ConnectionId, "Admins");
			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			
			Groups.Remove(Context.ConnectionId, "Admins");
			return base.OnDisconnected(stopCalled);
		}

	}
}