<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="friends.ascx.cs" Inherits="TheaterTemplate.Web.Controls.Friends.friends" %>
    <link rel="stylesheet" href="/friends/css/friends.css" />
    <script type="text/javascript" src="/friends/bootstrap/js/bootstrap.min.js"></script>


<script type="text/template" id="friends-account-template">

		<div class="container-fluid">
			<div class="row-fluid">
				<div class="span20">
					<[ if(this.appState.get('loggedIn')) { ]>
						<p class="lead">Hello, <[- this.user.get('first_name') ]></p>
						<span class="btn friends-logout-button">Logout from Facebook</span>
					<[ } else { ]>
						<span class="btn friends-login-button">Login to Facebook</span>
					<[ } ]>
				</div>
			</div>
		</div>

	</script>

	<script type="text/template" id="friends-picker-template">

		<div class="container-fluid">
			<div class="row-fluid">
				<[ if(this.appState.get('loggedIn')) { ]>
					<span class="btn friends-show-list">Pick Friends</span>
				<[ } else { ]>
					<span class="btn friends-login-button">Login to Facebook to Pick Friends</span>
				<[ } ]>
			</div>
		</div>

	</script>

	<script type="text/template" id="friends-picker-popover-template">
		<div class="friends-list">
			<[ this.user.friends.each(function (friend) { ]>
				<div class="form-inline">
					<label class="checkbox offset1">
						<input type="checkbox" />
						<[- friend.get('name') ]>
					</label>
				</div>
			<[ }); ]>
		</div>
		<br />
		<div class="row-fluid">
			<div class="span9">
				<span class="btn btn-small btn-block popover-close">cancel</span>
			</div>
			<div class="span9 offset2">
				<span class="btn btn-small btn-block popover-select">select</span>
			</div>
		</div>
	</script>

    <script type="text/javascript" src="/friends/friends/config.js"></script>
    <script type="text/javascript" src="/friends/friends/models.js"></script>
    <script type="text/javascript" src="/friends/friends/module.js"></script>
    <script type="text/javascript" src="/friends/friends/app.js"></script>
    <script type="text/javascript" src="/friends/friends/account.js"></script>
    <script type="text/javascript" src="/friends/friends/picker.js"></script>

    <script type="text/javascript" src="/friends/friends/fbstart.js"></script>