﻿// OpenTween - Client of Twitter
// Copyright (c) 2017 kim_upsilon (@kim_upsilon) <https://upsilo.net/~upsilon/>
// All rights reserved.
//
// This file is part of OpenTween.
//
// This program is free software; you can redistribute it and/or modify it
// under the terms of the GNU General Public License as published by the Free
// Software Foundation; either version 3 of the License, or (at your option)
// any later version.
//
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License
// for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program. If not, see <http://www.gnu.org/licenses/>, or write to
// the Free Software Foundation, Inc., 51 Franklin Street - Fifth Floor,
// Boston, MA 02110-1301, USA.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenTween
{
    public partial class LoginDialog : OTBaseForm
    {
        public string LoginName => this.textboxLoginName.Text;
        public string Password => this.textboxPassword.Text;

        public Func<Task<bool>> LoginCallback { get; set; } = null;
        public bool LoginSuccessed { get; set; } = false;

        public LoginDialog()
            => this.InitializeComponent();

        private async void buttonLogin_Click(object sender, EventArgs e)
        {
            if (this.LoginCallback == null)
                return;

            try
            {
                using (ControlTransaction.Disabled(this))
                {
                    // AcceptButton によって自動でフォームが閉じられるのを抑制する
                    this.AcceptButton = null;

                    this.LoginSuccessed = await this.LoginCallback();
                    if (this.LoginSuccessed)
                        this.DialogResult = DialogResult.OK;
                }
            }
            finally
            {
                this.AcceptButton = this.buttonLogin;
            }
        }
    }
}
