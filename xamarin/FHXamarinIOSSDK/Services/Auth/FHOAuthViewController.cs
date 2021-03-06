﻿using System;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Foundation;

namespace FHSDK.Services
{
    /// <summary>
    /// A view controller to handle OAuth login using UIWebView
    /// </summary>
	public class FHOAuthViewController: UIViewController, IUIWebViewDelegate
	{
		string authUrl;
		UIView topView;
		UINavigationBar titleBar;
		UIWebView webView;
		UIActivityIndicatorView activityView;
		OAuthClientHandlerService authHanlder;
		bool finished = false;
		bool cancelled = false;

		public FHOAuthViewController (string url, OAuthClientHandlerService handler)
			:base()
		{
			this.authUrl = url;
			this.authHanlder = handler;
		}

        /// <summary>
        /// Construct the UI
        /// </summary>
		public override void LoadView()
		{
			UIWindow appWindow = UIApplication.SharedApplication.Delegate.Window;
			float titlebarHeight = 45F;
			float appHeight = appWindow.Frame.Size.Height;
			float appWidth = appWindow.Frame.Size.Width;
			RectangleF frame = new RectangleF (0F, 0F, appWidth, appHeight);
			topView = new UIView (frame);
			topView.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;
			RectangleF barFrame = new RectangleF (0F, 0F, appWidth, titlebarHeight);
			titleBar = new UINavigationBar (barFrame);
			titleBar.BarStyle = UIBarStyle.Black;
			titleBar.AutoresizingMask = UIViewAutoresizing.FlexibleWidth;

			RectangleF webviewFrame = new RectangleF (0F, titlebarHeight, appWidth, appHeight - titleBar.Frame.Size.Height);
			webView = new UIWebView (webviewFrame);
			webView.ScalesPageToFit = true;

			RectangleF activityFrame = new RectangleF(appWidth/2 - 12.5f, appHeight/2 -12.5f, 25F, 25F);
			activityView = new UIActivityIndicatorView (activityFrame);
			activityView.ActivityIndicatorViewStyle = UIActivityIndicatorViewStyle.Gray;
			activityView.SizeToFit ();
			activityView.AutoresizingMask = UIViewAutoresizing.FlexibleLeftMargin | UIViewAutoresizing.FlexibleRightMargin | UIViewAutoresizing.FlexibleTopMargin | UIViewAutoresizing.FlexibleBottomMargin;

			topView.AddSubview (titleBar);
			topView.AddSubview (activityView);
			topView.AddSubview (webView);

			UINavigationItem titleBarItem = new UINavigationItem ("Login");
			UIBarButtonItem done = new UIBarButtonItem ("Close", UIBarButtonItemStyle.Done, delegate (object sender, EventArgs e) {
				cancelled = true;
				CloseView();
			});

			titleBarItem.SetLeftBarButtonItem (done, true);
			titleBar.PushNavigationItem (titleBarItem, true);

			this.View = topView;
		}

        /// <summary>
        /// Close the view
        /// </summary>
		public void CloseView()
		{
			this.PresentingViewController.DismissViewController (true, null);
			if (!this.finished) {
				this.authHanlder.OnFail (this.cancelled);
			}
		}

        /// <summary>
        /// Start to load request when view is loaded
        /// </summary>
		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
			this.webView.Delegate = new AuthDelegate (this);
			NSUrl reqeustUrl = new NSUrl (this.authUrl);
			this.webView.LoadRequest (new MonoTouch.Foundation.NSUrlRequest (reqeustUrl));
		}
			
		private void ShowActivityView()
		{
			this.topView.BringSubviewToFront (activityView);
			this.activityView.StartAnimating ();
		}

		private void HideActivityView()
		{
			this.activityView.StopAnimating ();
			this.topView.SendSubviewToBack (activityView);
		}

		class AuthDelegate : UIWebViewDelegate 
		{
			FHOAuthViewController controller;

			public AuthDelegate(FHOAuthViewController parent)
				:base()
			{
				this.controller = parent;
			}

			public override void LoadStarted(UIWebView webview)
			{
				this.controller.ShowActivityView ();
			}

			public override void LoadingFinished(UIWebView webview)
			{
				this.controller.HideActivityView ();
				if (this.controller.finished) {
					this.controller.CloseView ();
				}
			}

			public override bool ShouldStartLoad (UIWebView webView, MonoTouch.Foundation.NSUrlRequest request, UIWebViewNavigationType navigationType)
			{
				string urlStr = request.Url.AbsoluteString;
				Uri uri = new Uri (urlStr);
				string query = uri.Query;
				if (query.IndexOf ("status=complete") > -1) {
					this.controller.finished = true;
					this.controller.authHanlder.OnSuccess (uri);
				}
				return true;
			}

		}
	}
}

