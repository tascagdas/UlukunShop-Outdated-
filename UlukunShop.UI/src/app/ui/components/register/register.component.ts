import {Component, OnInit} from '@angular/core';
import {AbstractControl, UntypedFormBuilder, UntypedFormGroup, ValidationErrors, Validators} from "@angular/forms";
import {User} from "../../../entities/user";
import {UserService} from "../../../services/common/models/user.service";
import {Create_User} from "../../../contracts/users/create_user";
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "../../../services/ui/custom-toastr.service";
import {BaseComponent} from "../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends BaseComponent implements OnInit {

  constructor(private formBuilder: UntypedFormBuilder, private userService: UserService,
              private toastr:CustomToastrService, spinner:NgxSpinnerService) {
    super(spinner);
  }

  frm: UntypedFormGroup;

  ngOnInit(): void {
    this.frm = this.formBuilder.group({
      firstName: ['', [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50)
      ]],
      lastName: ['', [Validators.required,
        Validators.minLength(2),
        Validators.maxLength(50)
      ]],
      email: ['', [Validators.required,
        Validators.email,
        Validators.maxLength(50)
      ]],
      password: ['', [Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50)
      ]],
      rePassword: ['', [Validators.required,
        Validators.minLength(5),
        Validators.maxLength(50)
      ]],
      userName: ['', [Validators.required,
        Validators.minLength(3),
        Validators.maxLength(50)
      ]],
    }, {
      validators: (group: AbstractControl): ValidationErrors | null => {
        let password = group.get("password").value;
        let rePassword = group.get("rePassword").value;
        return password === rePassword ? null : {notSame: true};
      }
    })
  }

  get component() {
    return this.frm.controls;
  }


  isSubmitted: boolean = false;

  async onSubmit(user: User) {
    this.isSubmitted = true;

    if (this.frm.invalid)
      return;

    const result: Create_User = await this.userService.create(user)

    if (result.succeeded)
        this.toastr.message(result.message,"Kayit Basarili",{
          messageType:ToastrMessageType.Success,
          position:ToastrPosition.TopRight
        })
    else
      this.toastr.message(result.message,"Kayit Basarisiz",{
        messageType:ToastrMessageType.Error,
        position:ToastrPosition.TopRight
      })
  }
}
