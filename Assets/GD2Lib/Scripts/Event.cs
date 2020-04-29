using System;
using UnityEngine;

namespace GD2Lib {

    /// <summary>
    /// Event des GD2
    /// </summary>
    [CreateAssetMenu(fileName = "GD2-Event", menuName = "GD2Lib/Event/Evenement")]
    public class Event : ScriptableObject {

        /// <summary>
        /// Signature du delegate (histoire de pas utiliser que des Actions, vous avez un exemple par action dans les GenericVar ;)
        /// </summary>
        /// <param name="p_event">Evenement a l'origine de l'event</param>
        /// <param name="p_params">Paramètres de l'évènement</param>
        public delegate void OnRaiseDelegate(Event p_event, object[] p_params);

        /// <summary>
        /// Evènement
        /// </summary>
        [NonSerialized] protected OnRaiseDelegate OnRaise;

        /// <summary>
        /// Raise this event
        /// </summary>
        /// <param name="p_params">Optionnal parameters</param>

        public virtual void Raise(params object[] p_params) {
            // Ca c''est une technique différente pour ecrire en gros ca ->
            // if (OnRaise != null)
            //      OnRaise(this, params);
            OnRaise?.Invoke(this, p_params);
        }

        /// <summary>
        /// Inscrit un delegate
        /// </summary>
        /// <param name="p_delegate"></param>
        public virtual void Register(OnRaiseDelegate p_delegate) {
            OnRaise += p_delegate;
        }

        /// <summary>
        ///  Désinscrit un delegate
        /// </summary>
        /// <param name="p_delegate">Delegate a désinscrire de l'event</param>
        public virtual void Unregister(OnRaiseDelegate p_delegate) {
            if (p_delegate != null)
                OnRaise -= p_delegate;
        }


        // Ca c'est une region, pratique pour organiser  vos codes
        #region Parsing des arguments 


        /// <summary>
        /// Tente de récupérer le paramètre dans l'evenement
        /// </summary>
        /// <typeparam name="T">Type du paramètre</typeparam>
        /// <param name="o_param1">Paramètre retrouvé</param>
        /// <param name="p_params">Liste des paramètres de l'event</param>
        /// <returns></returns>
        public static bool TryParseArgs<T>(out T o_param1, object[] p_params) {

            if (IsLenghtCorrect(1, p_params)) {
                o_param1 = ConvertParam<T>(p_params[0]);
                return true;
            }

            o_param1 = default;
            return false;
        }

        /// <summary>
        /// Tente de récupérer les 2 paramètres de l'evenement
        /// </summary>
        /// <typeparam name="T">Type du premier paramètre</typeparam>
        /// <typeparam name="T1">Type du deuxième paramètre</typeparam>
        /// <param name="o_param1">1er paramètre</param>
        /// <param name="o_param2">2eme paramètre</param>
        /// <param name="p_params">Liste des paramètres de l'event</param>
        /// <returns></returns>
        public static bool TryParseArgs<T, T1>(out T o_param1, out T1 o_param2, object[] p_params) {

            if (IsLenghtCorrect(2, p_params)) {
                o_param1 = ConvertParam<T>(p_params[0]);
                o_param2 = ConvertParam<T1>(p_params[1]);
                return true;
            }

            o_param1 = default;
            o_param2 = default;
            return false;
        }

        /// <summary>
        /// Tente de récupérer les 3 paramètres de l'evenement
        /// </summary>
        /// <typeparam name="T">Type du premier paramètre</typeparam>
        /// <typeparam name="T1">Type du deuxième paramètre</typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="o_param1">1er paramètre</param>
        /// <param name="o_param2">2eme paramètre</param>
        /// <param name="o_param3">3eme paramètre</param>
        /// <param name="p_params">Liste des paramètres de l'event</param>
        /// <returns></returns>
        public static bool TryParseArgs<T, T1, T2>(out T o_param1, out T1 o_param2, out T2 o_param3, object[] p_params) {

            if (IsLenghtCorrect(3, p_params)) {
                o_param1 = ConvertParam<T>(p_params[0]);
                o_param2 = ConvertParam<T1>(p_params[1]);
                o_param3 = ConvertParam<T2>(p_params[2]);
                return true;
            }

            o_param1 = default;
            o_param2 = default;
            o_param3 = default;

            return false;
        }

        /// <summary>
        /// Tente de récupérer les 4 paramètres de l'evenement
        /// </summary>
        /// <typeparam name="T">Type du premier paramètre</typeparam>
        /// <typeparam name="T1">Type du deuxième paramètre</typeparam>
        /// <typeparam name="T2">Type du troisième paramètre</typeparam>
        /// <typeparam name="T3">Type du quatrième paramètre</typeparam>
        /// <param name="o_param1">1er paramètre</param>
        /// <param name="o_param2">2eme paramètre</param>
        /// <param name="o_param3">3eme paramètre</param>
        /// <param name="o_param4">4eme paramètre</param>
        /// <param name="p_params">Liste des paramètres de l'event</param>
        /// <returns></returns>
        public static bool TryParseArgs<T, T1, T2, T3>(out T o_param1, out T1 o_param2, out T2 o_param3, out T3 o_param4, object[] p_params) {

            if (IsLenghtCorrect(4, p_params)) {
                o_param1 = ConvertParam<T>(p_params[0]);
                o_param2 = ConvertParam<T1>(p_params[1]);
                o_param3 = ConvertParam<T2>(p_params[2]);
                o_param4 = ConvertParam<T3>(p_params[3]);
                return true;
            }

            o_param1 = default;
            o_param2 = default;
            o_param3 = default;
            o_param4 = default;

            return false;
        }


        /// <summary>
        /// Vérifie le nombre de paramètres
        /// </summary>
        /// <param name="p_targetLenght">Longeur attendu du tableau</param>
        /// <param name="p_params">Liste des paramètres</param>
        /// <returns></returns>
        static bool IsLenghtCorrect(int p_targetLenght, object[] p_params) {
            if (p_params != null && p_params.Length >= p_targetLenght)
                return true;
            return false;
        }

        /// <summary>
        /// Converti un paramètre dans le type donné
        /// </summary>
        /// <typeparam name="T">Type de donnée souhaité</typeparam>
        /// <param name="p_param">Paramètre a convertir</param>
        /// <returns></returns>
        static T ConvertParam<T>(object p_param) {

#if UNITY_EDITOR
            // Ca c'est juste un test pour vous éviter de faire n'importe quoi et d'avoir des erreurs zarbi.
            // Test un peu lourd donc je ne l'effectue que dans l'éditeur pour avoir de meilleurs performances dans votre jeux final sur les event
            if (p_param != null && !typeof(T).IsAssignableFrom(p_param.GetType())) 
                return default;
#endif

            try {
                return p_param != null ? (T)p_param : default;
            }
            catch (Exception ex) {
                Debug.LogException(ex);
                return default;
            }
        }

        #endregion Parsing des arguments 

    }
}