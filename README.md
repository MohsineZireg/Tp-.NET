# Readme - TP .NET et Serverless

## Membres du binôme :

- Defaa Fayssal
- Zireg Mohsine

## Commandes cURL :

1.  L'authentification de la Function App est configurée sur AuthorizationLevel.Function. Il est nécessaire de posséder la clé API. Vous pouvez l'obtenir [ici](https://portal.azure.com/#@MngEnv877738.onmicrosoft.com/resource/subscriptions/d015b0b8-e1a4-4d29-95fd-e64a89f63b91/resourceGroups/mohsine-fa_group/providers/Microsoft.Web/sites/mohsine-fa/functionsAppKeys), sinon je peux vous l'envoyer si vous n'avez pas accès à la ressource.

2.  Ensuite, voici la commande cURL à exécuter, supposant que vous souhaitez redimensionner une image `input0.jpg` dans votre répertoire actuel et obtenir `output1.jpeg` :

```bash
curl --data-binary "@input0.jpg" -H "x-functions-key: $API_KEY" -X POST "https://mohsine-fa.azurewebsites.net/api/ResizeHttpTrigger?w=100&h=100" -v > output1.jpeg
```

## Liens des ressources Azure:

1.  Function App Resize: [mohsine-fa](https://portal.azure.com/#@MngEnv877738.onmicrosoft.com/resource/subscriptions/d015b0b8-e1a4-4d29-95fd-e64a89f63b91/resourceGroups/mohsine-fa_group/providers/Microsoft.Web/sites/mohsine-fa/appServices)

2.  Blob: [blobfm](https://portal.azure.com/#@MngEnv877738.onmicrosoft.com/resource/subscriptions/d015b0b8-e1a4-4d29-95fd-e64a89f63b91/resourceGroups/mohsine-fa_group/providers/Microsoft.Storage/storageAccounts/blobfm)

3.  Resize Logic App: [resize-logic-app-final](https://portal.azure.com/#@MngEnv877738.onmicrosoft.com/resource/subscriptions/d015b0b8-e1a4-4d29-95fd-e64a89f63b91/resourceGroups/mohsine-fa_group/providers/Microsoft.Logic/workflows/resize-logic-app-final)

## Vidéo Bonus:

Vous trouverez dans le dossier `public/demo` les 2 vidéos des démonstrations demandées.
