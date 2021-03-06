"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require('@angular/core');
var common_1 = require('@angular/common');
var router_deprecated_1 = require('@angular/router-deprecated');
var routes_1 = require('./routes');
var Account = (function () {
    function Account() {
    }
    Account = __decorate([
        core_1.Component({
            selector: 'account',
            templateUrl: './app/components/account/account.html',
            directives: [router_deprecated_1.ROUTER_DIRECTIVES, common_1.CORE_DIRECTIVES, common_1.FORM_DIRECTIVES]
        }),
        router_deprecated_1.RouteConfig(routes_1.APP_ROUTES), 
        __metadata('design:paramtypes', [])
    ], Account);
    return Account;
}());
exports.Account = Account;
//# sourceMappingURL=account.js.map