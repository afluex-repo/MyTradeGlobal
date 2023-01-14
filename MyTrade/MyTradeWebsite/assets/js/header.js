document.write(`
<!-- Meta -->
		<meta charset="utf-8">
		<meta http-equiv="X-UA-Compatible" content="IE=edge">
		<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
		<meta name="description" content="">
		<meta name="keywords" content="">		
		<!-- SITE TITLE -->
		<title>My Trade</title>			
		<!-- Latest Bootstrap min CSS -->
		<link rel="stylesheet" href="assets/bootstrap/css/bootstrap.min.css">		
		<!-- Google Font -->
		<link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300;400;500;600;700;800;900&amp;display=swap" rel="stylesheet">  
		<link rel="icon" type="image/png" href="assets/img/favicon.png">
		<!-- Font Awesome CSS -->
		<link rel="stylesheet" href="assets/fonts/font-awesome.min.css">
		<link rel="stylesheet" href="assets/fonts/themify-icons.css">
		<!--- owl carousel Css-->
		<link rel="stylesheet" href="assets/owlcarousel/css/owl.carousel.css">
		<link rel="stylesheet" href="assets/owlcarousel/css/owl.theme.css">
		<!--slicknav Css-->
        <link rel="stylesheet" href="assets/css/slicknav.css">		
		<!-- animate CSS -->
		<link rel="stylesheet" href="assets/css/animate.css">	
		<!-- Style CSS -->		
		<link rel="stylesheet" href="assets/css/style.css">			
		<link rel="stylesheet" href="assets/css/responsive.css">
        <!--Popup CSS-->
		<link rel="stylesheet" href="assets/css/popup.css">

        <div class="body-particles"></div>

        <!-- particles.js container -->
        <div id="particles-js"></div>	
		<!-- START PRELOADER -->
		<div class="preloader">
			<div class="status">
				<div class="status-mes"></div>
			</div>
		</div>
		<!-- END PRELOADER -->		

		<!-- START NAVBAR -->  
		<div id="navigation" class="fixed-top navbar-light bg-faded site-navigation">
			<div class="container">
				<div class="row">
					<div class="col-lg-3 col-md-3 col-sm-4">
						<div class="site-logo">
							<a class="navbar-logo" href="index.html"><img src="assets/img/logo-2.png" alt=""></a>
						</div>
					</div><!--- END Col -->					
					<div class="col-lg-9 col-md-9 col-sm-8">
						<div class="header_right">
							<nav id="main-menu" class="ml-auto">
								<ul>
								<li><a href="index.html">Home</a></li>
								<li><a href="index.html#about">About</a></li>
								<li><a href="index.html#forex">Forex</a></li>
								<li><a href="index.html#business">Business</a></li>
								<li><a href="index.html#faq">faq</a></li>	
								<li><a href="index.html#download">Get App</a></li>									  
								<li><a href="index.html#contact">Contact Us</a></li>
								<li><a class ="block-menu" id="modal_trigger" href="#modal">Login</a></li>
								</ul>
							</nav>
							<div id="mobile_menu"></div>
						</div>
					</div><!--- END Col -->
				</div><!--- END ROW -->
			</div><!--- END CONTAINER -->
		</div> 	  
		<!-- END NAVBAR -->

        <!--START LOGIN-->
		<div id="modal" class ="popupContainer" style="position:fixed;z-index:2;
    opacity: 1;
    z-index: 11000;
    left: 50%;
    margin-left: -165px;
    top: 100px;">
			<header class ="popupHeader">
				<span class ="header_title">Login</span>
				<span class ="modal_close"><i class ="fa fa-times"></i></span>
			</header>

			<div class ="popupBody">
				<!--Social Login-->
				<div class ="social_login">
					<div class ="">
						<a href="#" class ="social_box fb">
							<span class ="icon"><i class ="fa fa-facebook"></i></span>
							<span class ="icon_title">Connect with Facebook</span>
						</a>

						<a href="#" class ="social_box google">
							<span class ="icon"><i class ="fa fa-google-plus"></i></span>
							<span class ="icon_title">Connect with Google</span>
						</a>
					</div>

					<div class ="centeredText">
						<span>Or use your Email address</span>
					</div>

					<div class ="action_btns">
						<div class ="one_half"><a href="#" id="login_form" class ="abtn">Login</a></div>
						<div class ="one_half last"><a href="#" id="register_form" class ="abtn">Sign up</a></div>
					</div>
				</div>

				<!--Username & Password Login form-->
				<div class ="user_login">
					<form>
						<label>Email / Username</label>
						<input type="text" />
						<br />

						<label>Password</label>
						<input type="password" />
						<br />

						<div class ="checkbox">
							<input id="remember" type="checkbox" />
							<label for="remember">Remember me on this computer</label>
						</div>

						<div class ="action_btns">
							<div class ="one_half"><a href="#" class ="abtn back_btn"><i class ="fa fa-angle-double-left"></i> Back</a></div>
							<div class ="one_half last"><a href="#" class ="abtn">Login</a></div>
						</div>
					</form>

					<a href="#" class ="forgot_password">Forgot password?</a>
				</div>

				<!--Register Form-->
				<div class ="user_register">
					<form>
						<label>Full Name</label>
						<input type="text" />
						<br />

						<label>Email Address</label>
						<input type="email" />
						<br />

						<label>Password</label>
						<input type="password" />
						<br />

						<div class ="checkbox">
							<input id="send_updates" type="checkbox" />
							<label for="send_updates">Send me occasional email updates</label>
						</div>

						<div class ="action_btns">
							<div class ="one_half"><a href="#" class ="abtn back_btn"><i class ="fa fa-angle-double-left"></i> Back</a></div>
							<div class ="one_half last"><a href="#" class ="abtn">Register</a></div>
						</div>
					</form>
				</div>
			</div>
		</div>
		<!--END LOGIN-->


`);