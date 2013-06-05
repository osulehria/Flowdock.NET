﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NUnit.Framework;
using Flowdock.ViewModels;
using MoqaLate.Autogenerated;

namespace Flowdock.UnitTests.ViewModels {
	public class LoginCommandTest {
		private FlowdockContextMoqaLate _flowdockContext;
		private LoginViewModel _loginViewModel;
		private LoginCommand _command;

		[SetUp]
		protected void BeforeEach() {
			_flowdockContext = new FlowdockContextMoqaLate();
			_loginViewModel = new LoginViewModel(_flowdockContext, new AppSettingsMoqaLate(), new NavigationManagerMoqaLate());
			_command = new LoginCommand(_loginViewModel, _flowdockContext);
		}

		[Test]
		public void CanExecute_false_if_username_and_password_are_null() {
			_loginViewModel.Username = null;
			_loginViewModel.Password = null;

			Assert.That(_command.CanExecute(null), Is.False);
		}

		[Test]
		public void CanExecute_false_if_username_and_password_are_blank() {
			_loginViewModel.Username = "";
			_loginViewModel.Password = "";

			Assert.That(_command.CanExecute(null), Is.False);
		}

		[Test]
		public void CanExecute_false_if_username_and_password_are_whitespace() {
			_loginViewModel.Username = "   ";
			_loginViewModel.Password = "      ";

			Assert.That(_command.CanExecute(null), Is.False);
		}

		[Test]
		public void CanExecute_true_if_username_and_password_are_set() {
			_loginViewModel.Username = "hello@kitty.com";
			_loginViewModel.Password = "somepassword";

			Assert.That(_command.CanExecute(null), Is.True);
		}

		[Test]
		public void Execute_does_not_execute_if_cant() {
			_loginViewModel.Username = "";
			_loginViewModel.Password = "";

			_command.Execute(null);
			Assert.That(_flowdockContext.LoginWasCalled(), Is.False);
		}

		[Test]
		public void Execute_does_execute_if_it_can() {
			_loginViewModel.Username = "someuser";
			_loginViewModel.Password = "somepassword";

			_command.Execute(null);
			Assert.That(_flowdockContext.LoginWasCalled(), Is.True);
		}
	}
}