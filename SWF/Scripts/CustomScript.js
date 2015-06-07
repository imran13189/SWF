var mainContentHolder,
    openedPopups,
    loggedInUserRole;

$(document).ready(function () {
    mainContentHolder = $(document).find('section.main-content');
    openedPopups = [];
    loggedInUserRole = $('header').find('#hdnRoleName');

    loggedInUserRole = ((loggedInUserRole.length > 0) ? loggedInUserRole.val() : '');

    $(window).scroll(function () {
        if ($(window).scrollTop() > $('.header').offset().top && !($('.header').hasClass('posi'))) {
            $('.header').addClass('posi');
        } else if ($(window).scrollTop() == 0) {
            $('.header').removeClass('posi');
        }
    });

    mainContentHolder.tooltip({
        selector: "[title]",
        placement: "left",
        trigger: "focus",
        animation: false
    });

    mainContentHolder.on('click', '.projects-grid .btn-delete', function () {
        var selectedRow = $(this).parents('tr'),
            selectedProject = selectedRow.data('id');

        $.ajax({
            url: '/Translations/Delete',
            type: 'GET',
            cache: false,
            data: { id: parseInt(selectedProject, 10) },
            success: function (htmlResult) {
                mainContentHolder.find('.grid-holder').html(htmlResult);
            },
            complete: function () {
            }
        });
    });

    mainContentHolder.on('change', '#ddlProjectStatus', function () {
        var dropDown = $(this);
        //dropDown.parents('form').trigger('submit');

        if (dropDown.val().length > 0) {
            $.ajax({
                url: '/Admin/Dashboard/GetProjects',
                type: 'GET',
                cache: false,
                data: { status: parseInt(dropDown.val(), 10) },
                success: function (htmlResult) {
                    mainContentHolder.find('.grid-holder').html(htmlResult);
                },
                complete: function () {
                }
            })
        }

    });

    mainContentHolder.on('click', '.available-translator', function () {
        debugger;
        var activeElement = $(this),
            receiverId = activeElement.data('id'),
            popupName = 'conversationPopup-' + receiverId,
            popup = isPopupAlreadyOpened(popupName);

        if (popup === null) {
            popup = window.open('', popupName, 'width=420,height=220,scrollbars=0,toolbars=0,titlebar=0,menubar=0,resizable=0');
            openedPopups.push(popup);
            popup.focus();
            activeElement.parents('form').trigger('submit');
            popup.onbeforeunload = function () {
                removePopup(popupName);
            };
        }
        else {
            popup.focus();
        }


    });

    if ((loggedInUserRole === 'Client') || (loggedInUserRole === 'Translator')) {
        var checkMessages = setInterval(function () {
            $.ajax({
                url: '/Utility/CheckMessages',
                type: 'get',
                cache: false,
                success: function (result) {
                },
                error: function (jqXhr, settings, exception) {
                }
            });
        }, 15000);
    }

    var loginForm = mainContentHolder.find('#frmLogin');

    if (loginForm.length > 0) {
        loginForm.data('validator').settings.ignore = '';
    }

    var claimTranslationForm = mainContentHolder.find('#frmAddClaimTranslation');

    if (claimTranslationForm.length > 0) {
        debugger;
        claimTranslationForm.find('#DeliveryDate').datepicker({
            format: 'mm/dd/yyyy',
            startDate: '+0d',
            autoclose: true
        });
        if (claimTranslationForm.find('#anchDocumentDownload').length > 0) {
            claimTranslationForm.find('#fileDocumentUpload').removeAttr('data-val').removeAttr('data-val-required');
            claimTranslationForm.data('validator').settings.ignore = ':hidden, :file';
        }
    }
    var AdminAssignTranslatorForm = mainContentHolder.find('#frmAssignTranslator');
    if (AdminAssignTranslatorForm.length > 0) {
        debugger;
        AdminAssignTranslatorForm.find('#DeliveryDate').datepicker({
            format: 'mm/dd/yyyy',
            startDate: '+0d',
            autoclose: true
        });
    }

    var Edittranslatorform = mainContentHolder.find('#frmEditTranslator');
    if (Edittranslatorform.length > 0) {
        debugger;
        Edittranslatorform.find('#EstimateApprovalDate').datepicker({
            format: 'mm/dd/yyyy',
            startDate: '+0d',
            autoclose: true
        });
        Edittranslatorform.find('#EstimatedDeliveryDate').datepicker({
            format: 'mm/dd/yyyy',
            startDate: '+0d',
            autoclose: true
        });
    }

    var clientTranslatorRegistrationForm = mainContentHolder.find('#frmAddEditTranslatorClient');

    if (clientTranslatorRegistrationForm.length > 0) {
        clientTranslatorRegistrationForm.data('validator').settings.ignore = ':hidden, :checkbox'
    }

    var userRegistrationForm = mainContentHolder.find('#frmRegister');

    if (userRegistrationForm.length > 0) {
        userRegistrationForm.on('change', '[name="UserAddressModel.CountryId"]', function () {
            var dropDown = $(this);

            if (dropDown.val().length > 0) {
                $.ajax({
                    url: '/Utility/GetCities',
                    type: 'GET',
                    cache: false,
                    data: { countryId: parseInt(dropDown.val(), 10) },
                    success: function (htmlResult) {
                        userRegistrationForm.find('[name="UserAddressModel.CityId"]').html(htmlResult);
                    },
                    complete: function () {
                    }
                });
            }
        });
    }
});

function validateLogin() {
    var username = mainContentHolder.find('#frmLogin [name="text1"]'),
        password = mainContentHolder.find('#frmLogin [name="text2"]');

    if ((username.val().length > 0) && (password.val().length > 0)) {
        mainContentHolder.find('#frmLogin [name="Username"]').val(username.val()).end()
            .find('#frmLogin [name="Password"]').val(password.val());
        return true;
    }
}


function isPopupAlreadyOpened(popupName) {
    debugger;
    for (var idx = 0, limit = openedPopups.length; idx < limit; idx++) {
        if (openedPopups[idx].name === popupName) {
            return openedPopups[idx];
        }
    }

    return null;
}

function removePopup(popupName) {
    debugger;
    for (var idx = 0, limit = openedPopups.length; idx < limit; idx++) {
        if (openedPopups[idx].name === popupName) {
            openedPopups.splice(idx, 1);
            return false;
        }
    }
}