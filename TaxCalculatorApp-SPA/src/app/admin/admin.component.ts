import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { PostalCode } from '../_models/PostalCode';
import { AdminService } from '../_services/admin.service';
import { AlertifyService } from '../_services/alertify.service';
import { TaxCalculationType } from '../_models/TaxCalculationType';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css']
})
export class AdminComponent implements OnInit {
  postalCodes: PostalCode[];
  taxCalculationTypes: TaxCalculationType[];
  public bsModalRef: BsModalRef;
  selectedOption: string;
  selectedOptionId: number;
  CRUDstate: string;
  BTNstate: string;
  model: any = {};
  postalCode: string;
  taxCalculationType: string;
  postalCodeObj: any = {};
  defaultType: any;

  constructor(
    private adminService: AdminService,
    private alertify: AlertifyService,
    private modalService: BsModalService
  )  {}

  ngOnInit() {
    this.getPostalCodes();
    this.getTaxCalculationTypes();
  }

  getPostalCodes() {
    this.adminService.getPostalCodes().subscribe(
      (postalCodes: PostalCode[]) => {
        this.postalCodes = postalCodes;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  getTaxCalculationTypes() {
    this.adminService.getTaxCalculationTypes().subscribe(
      (taxCalculationTypes: TaxCalculationType[]) => {
        this.taxCalculationTypes = taxCalculationTypes;
      },
      error => {
        this.alertify.error(error);
      }
    );
  }

  public openModal(template: TemplateRef<any>) {
    this.bsModalRef = this.modalService.show(template);
  }

  onSelected(item) {
    this.selectedOption = item.type;
    this.selectedOptionId = item.id;
  }

  save() {
    this.model.taxCalculationTypeId = this.selectedOptionId;
    this.adminService.addPostalCode(this.model).subscribe(
      () => {
        this.getPostalCodes();
      },
      error => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  getPostalCode(item: any) {
    this.postalCodeObj = item;
  }

  delete(id: number) {
    this.adminService.deletePostalCode(id).subscribe(
      () => {
        this.getPostalCodes();
        this.alertify.success('Postal code deleted successfully');
      },
      error => {
        this.alertify.error(error);
      }
    );
    this.bsModalRef.hide();
  }

  confirmDelete(item: any) {
    this.delete(item.id);
  }

  addMode() {
    this.CRUDstate = 'Add';
    this.BTNstate = 'Save';
    this.model.code = '';
  }

  editMode(item: any) {
    this.selectedOption = item;
    this.model.code = item.code;
    this.CRUDstate = 'Edit';
    this.BTNstate = 'Update';
    this.selectedOptionId = +this.taxCalculationTypes.filter(i => i.id === item.taxTypeId)[0].id;
  }
}
