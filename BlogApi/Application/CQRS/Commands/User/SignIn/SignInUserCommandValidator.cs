using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.CQRS.Commands.User.SignIn
{
    public class SignInUserCommandValidator : AbstractValidator<SignInUserCommandRequest>
    {
        public SignInUserCommandValidator()
        {
            RuleFor(p => p.kullaniciAdi)
                .NotNull()
                    .WithMessage("Kullanıcı adı boş geçilemez.")
                .MinimumLength(5)
                    .WithMessage("Kullanıcı adı minimum 5 karakter olmalıdır.")
                .MaximumLength(15)
                    .WithMessage("Kullanıcı adı maximum 15 karakter uzunluğunda olmalıdır.")
                .Must(x =>
                    {
                        string ozelKarakterler = ",_?=)(/&%+^'!é\\<>£#$½{[]}\"|@*";
                        foreach (var item in ozelKarakterler)
                        {
                            foreach (var item2 in x)
                            {
                                if (item.Equals(item2))
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    })
                    .WithMessage("Kullanıcı adı - ve . dışında özel karakter içeremez")
                .Must(ad => !ad.Contains(" "))
                    .WithMessage("Kullanıcı adı boşluk içeremez.");




            RuleFor(p => p.parola)
                .NotNull()
                    .WithMessage("Şifre boş geçilemez.")
                .MinimumLength(6)
                    .WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.")
                .MaximumLength(30)
                    .WithMessage("Şifre maksimum 30 karakter uzunluğunda olabilir.")
                .Must(x => !x.Contains(" "))
                    .WithMessage("Şifre boşluk içeremez.")
                .Must(x =>
                {
                    string sayilar = "1234567890";
                    foreach (var item in sayilar)
                    {
                        foreach (var item2 in x)
                        {
                            if (item.Equals(item2))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                })
                    .WithMessage("Şifre en az bir rakam içermelidir.")
                .Must(x =>
                {
                    string buyukHarfler = "ABCDEFGHİJKLMNOPRSTUVYZWXÇĞİÖÜ";
                    foreach (var item in buyukHarfler)
                    {
                        foreach (var item2 in x)
                        {
                            if (item.Equals(item2))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                })
                    .WithMessage("Şifre en az bir büyük harf içermelidir.")
                .Must(x =>
                {
                    string ozelKarakterler = "abcdefghijklmnoprsştuvyzwxçğiöü";
                    foreach (var item in ozelKarakterler)
                    {
                        foreach (var item2 in x)
                        {
                            if (item.Equals(item2))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                })
                    .WithMessage("Şifre en az bir küçük harf içermelidir.")
                .Must(x =>
                {
                    string ozelKarakterler = "!^+%&()=?_|][½#£><é-,@.*";
                    foreach (var item in ozelKarakterler)
                    {
                        foreach (var item2 in x)
                        {
                            if (item.Equals(item2))
                            {
                                return true;
                            }
                        }
                    }
                    return false;
                })
                    .WithMessage("Şifre en az bir özel karakter içermelidir.")
                .Must(x =>
                {
                    string ozelKarakterler = "'}\"/\\(){$";
                    foreach (var item in ozelKarakterler)
                    {
                        foreach (var item2 in x)
                        {
                            if (item.Equals(item2))
                            {
                                return false;
                            }
                        }
                    }
                    return true;
                })
                    .WithMessage("Şifre '\"/(){}$\\ karakterlerini içeremez.");
                
        }
    }
}
