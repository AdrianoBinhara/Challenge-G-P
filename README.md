# Challenge G&P 🚀
---
Aplicativo criado para controle de estoque 💹


## Sobre o App 📱
- O aplicativo foi construido no padrao MVVM, utilizando Xamarin.Forms no mobile e ASP.NETCore para a API, com .NET5.0.
- O backend do aplicativo foi publicado no Heroku, por fins de praticidade.


## Algumas especificações 📝
Para usuários do **VS 2022**, é necessário uma tratativa na IDE para rodar o aplicativo, por conta de um bug conhecido que impede a execução.

Para que o app rode sem problemas, é necessário **desativar o Hot Reload** seguindo estes passos:
 1. Ferramentas 
 2. Opções 
 3. Debugging 
 4. XAML Hot Reload

Link para o bug reportado: [Bug](https://developercommunity.visualstudio.com/t/bug-in-visual-studio-2022-xamarin-signalr-method-n/1528510)



## Pacotes utilizados
- [```Refit (6.1.15)```](https://github.com/reactiveui/refit)
- [```Xamarin.Forms (5.0)```](https://github.com/xamarin/Xamarin.Forms)
- [```Acr.UserDialogs (7.2.0)```](https://github.com/aritchie/userdialogs)

---


## Telas Vertical
| ![Page1](Resources/splash.png)  | ![Page2](Resources/items.png) | ![Page3](Resources/add.png)
|:---:|:---:|:---:|
| Splash Screen | Tela de login | Listagem de ordens |

## Chamadas API
O aplicativo utiliza o pacote [```Refit```](https://github.com/reactiveui/refit) para chamadas API REST, por possuir uma implementação fácil, código limpo e legível.
Facilita também a criação de testes unitários.
```C#
 [[Get("/")]
 Task<List<ItemsModel>> GetItems();
```

## Avisos no app
O pacote [```Acr.UserDialogs```](https://github.com/aritchie/userdialogs) é responsável pela exibição de toasts. Diferente da implementação nativa, ele possui um visual moderno e de fácil estilização.
![Toast](Resources/toast.png)

### Possíveis erros ⚠️
- O backend está hospedado em um serviço gratuito do Heroku, que desliga automaticamente após 30 minutos de inatividade. Por conta disso, a primeira requisição feita pelo app pode demorar um pouco mais que as demais requisições posteriores.
---
 

## Comentários finais. 💬
- O aplicativo é distribuido na versão Android, e iOS.
- O aplicativo pode ser utilizado tanto na vertical como na horizontal.
- A performance da listagem de ordens deve ser testada no modo ```Release```, que é o padrão para este cenário.
- o App deve ser testado em um dispositivo real para testar a performance qualitativamente.

--- 

## Tópicos
- [# Clear DevChallenge](#-clear-devchallenge)
- [Sobre o App](#sobre-o-app)
- [Algumas especificações](#algumas-especificações)
- [Pacotes utilizados](#pacotes-utilizados)
- [Telas Vertical](#telas-vertical)
- [Telas Horizontal](#telas-horizontal)
- [Chamadas API](#chamadas-api)
- [Avisos no app](#avisos-no-app)
- [Testes](#testes)
  - [Pacotes de teste](#pacotes-de-teste)
- [Comentários finais.](#comentários-finais)
  - [Tópicos](#tópicos)
